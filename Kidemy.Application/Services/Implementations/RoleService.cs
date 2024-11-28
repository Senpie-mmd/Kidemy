using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Role;
using Kidemy.Domain.Events.Identity;
using Kidemy.Domain.Interfaces.Identity;
using Kidemy.Domain.Interfaces.User;
using Kidemy.Domain.Models.Identity;
using Kidemy.Domain.Models.User;
using Kidemy.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Kidemy.Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        #region Fields

        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RoleService> _logger;
        private readonly ILocalizingService _localizingService;
        private readonly IMediatorHandler _mediatorHandler;

        #endregion

        #region Constructor

        public RoleService(IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IUserRepository userRepository,
            ILogger<RoleService> logger,
            ILocalizingService localizingService,
            IMediatorHandler mediatorHandler)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
            _logger = logger;
            _localizingService = localizingService;
            _mediatorHandler = mediatorHandler;
        }

        #endregion

        #region Methods

        public async Task<Result<bool>> UserHasPermissionAsync(int userId, string permissionName)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(permissionName))
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var user = await _userRepository.GetByIdAsync(userId, includeProperties: nameof(User.Roles));

            if (user is null)
            {
                _logger.LogError($"Not found user by id: {userId}");
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var permission = await _permissionRepository
                .FirstOrDefaultAsync(p => p.UniqueName == permissionName, includeProperties: nameof(Permission.Roles));

            if (permission is null)
            {
                _logger.LogError($"Not found permission by uniqe name: {permissionName}");
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            return user.Roles
                ?.Any(userRole => permission.Roles?.Any(rolePermission => userRole.RoleId == rolePermission.RoleId) ?? false) ?? false;
        }

        public async Task<Result<List<AdminSideRoleViewModel>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            if (roles?.Any() ?? false)
            {
                var model = roles.Select(role => new AdminSideRoleViewModel().MapFrom(role)).ToList();

                await _localizingService.TranslateModelAsync(model);

                return model;
            }
            else
            {
                return new List<AdminSideRoleViewModel>();
            }


        }

        public async Task<Result<FilterRoleViewModel>> FilterRolesAsync(FilterRoleViewModel model)
        {
            if (model is null) return Result.Failure<FilterRoleViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditaions = Filter.GenerateConditions<Role>();

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                filterConditaions.Add(role => role.Title.Contains(model.Title));
            }

            if (!string.IsNullOrWhiteSpace(model.UniqueName))
            {
                filterConditaions.Add(role => role.UniqueName.Contains(model.UniqueName));
            }

            await _roleRepository.FilterAsync(model, filterConditaions, RoleMapper.MapRoleViewModel);

            await _localizingService.TranslateModelAsync(model.Entities);

            return model;
        }

        public async Task<Result> DeleteRoleAsync(int id)
        {
            if (id <= 0) return Result.Failure(ErrorMessages.ProcessFailedError);

            await _roleRepository.SoftDelete(id);
            await _roleRepository.SaveAsync();

            var RoleDeletedEvent = new RoleDeletedEvent(id);
            await _mediatorHandler.PublishEvent(RoleDeletedEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateRoleAsync(AdminSideUpsertRoleViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (model.PermissionsId is null || !model.PermissionsId.Any(id => id > 0))
            {
                return Result.Failure(ErrorMessages.YouMustSelectAtLeastOneAccessError);
            }

            var role = await _roleRepository.GetByIdAsync(model.Id, includeProperties: nameof(Role.Permissions));
            var roleCopied = role.DeepCopy<Role>();

            if (role is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            if (role.UniqueName != model.UniqueName)
            {
                var roleWithTheSameUniqueName = await _roleRepository.FirstOrDefaultAsync(r => r.UniqueName == model.UniqueName && r.Id != model.Id);
                if (roleWithTheSameUniqueName is not null)
                {
                    return Result.Failure(ErrorMessages.UniqueNameIsDuplicatedError);
                }
            }

            role.Title = model.Title;
            role.UniqueName = model.UniqueName;
            role.UpdatedDateOnUtc = DateTime.UtcNow;

            UpdateRolesPermissions(role, model.PermissionsId);

            _roleRepository.Update(role);
            await _roleRepository.SaveAsync();

            await _localizingService.SaveLocalizations(role, model);

            var roleUpdatedEvent = new RoleUpdatedEvent(roleCopied.Id, model.Title, roleCopied.Title, model.UniqueName, roleCopied.UniqueName, model.PermissionsId, null);
            await _mediatorHandler.PublishEvent(roleUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> CreateRoleAsync(AdminSideUpsertRoleViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var roleWithTheSameUniqueName = await _roleRepository.FirstOrDefaultAsync(r => r.UniqueName == model.UniqueName && r.Id != model.Id);
            if (roleWithTheSameUniqueName is not null)
            {
                return Result.Failure(ErrorMessages.UniqueNameIsDuplicatedError);
            }

            if (model.PermissionsId is null || !model.PermissionsId.Any(id => id > 0))
            {
                return Result.Failure(ErrorMessages.YouMustSelectAtLeastOneAccessError);
            }

            var role = new Role();
            role.Title = model.Title;
            role.UniqueName = model.UniqueName;

            UpdateRolesPermissions(role, model.PermissionsId);

            await _roleRepository.InsertAsync(role);
            await _roleRepository.SaveAsync();

            await _localizingService.SaveLocalizations(role, model);

            var roleCreateEvent = new RoleCreateEvent(role.Id, role.Title, role.UniqueName, model.PermissionsId);
            await _mediatorHandler.PublishEvent(roleCreateEvent);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpsertRoleViewModel>> GetRoleById(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertRoleViewModel>(ErrorMessages.ProcessFailedError);

            var role = await _roleRepository.GetByIdAsync(id, includeProperties: nameof(Role.Permissions));

            if (role is null) return Result.Failure<AdminSideUpsertRoleViewModel>(ErrorMessages.ProcessFailedError);

            var model = new AdminSideUpsertRoleViewModel().MapFrom(role);

            await _localizingService.FillModelToEditByAdminAsync(role, model);

            return model;
        }

        public async Task<Result<bool>> UserIsMasterNotAsync(int userId)
        {
            if (userId <= 0 || userId == null)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var user = await _userRepository.GetByIdAsync(userId, includeProperties: nameof(User.Roles));

            if (user is null)
            {
                _logger.LogError($"Not found user by id: {userId}");
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            if (user.Roles.Any(x => x.RoleId == 2))
            {
                return true;
            }
            return Result.Failure<bool>(ErrorMessages.ProcessFailedError);

        }


        #endregion

        #region Utilities

        private void UpdateRolesPermissions(Role role, List<int>? permissionsId)
        {
            if (permissionsId?.Any() ?? false)
            {
                var permissionsHaveBeenChanged = role.Permissions?.Count != permissionsId.Count ||
                    (role.Permissions?.Any(permission => !permissionsId.Contains(permission.PermissionId)) ?? true);

                if (permissionsHaveBeenChanged)
                {
                    // delete expected permissions
                    var deletedPermissions = role.Permissions?.Where(p => !permissionsId.Contains(p.PermissionId)) ?? new List<RolePermissionMapping>();
                    foreach (var permission in deletedPermissions)
                    {
                        role.Permissions?.Remove(permission);
                    }

                    // add new permissions
                    var newPermissionsId = permissionsId.Where(id => role.Permissions?.All(p => p.PermissionId != id) ?? true)
                                                        .ToList();

                    role.Permissions ??= new List<RolePermissionMapping>();

                    foreach (var permissionId in newPermissionsId)
                    {
                        role.Permissions.Add(new RolePermissionMapping
                        {
                            PermissionId = permissionId,
                            RoleId = role.Id,
                            Role = role
                        });
                    }
                }
            }
            else
            {
                role.Permissions?.Clear();
            }
        }

        public async Task<bool> BoolUserHasPermissionAsync(int userId, string permissionName)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(permissionName))
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(userId, includeProperties: nameof(User.Roles));

            if (user is null)
            {
                _logger.LogError($"Not found user by id: {userId}");
                return false;
            }

            var permission = await _permissionRepository
                .FirstOrDefaultAsync(p => p.UniqueName == permissionName, includeProperties: nameof(Permission.Roles));

            if (permission is null)
            {
                _logger.LogError($"Not found permission by uniqe name: {permissionName}");
                return false;
            }

            return user.Roles
                ?.Any(userRole => permission.Roles?.Any(rolePermission => userRole.RoleId == rolePermission.RoleId) ?? false) ?? false;
        }

        #endregion
    }
}
