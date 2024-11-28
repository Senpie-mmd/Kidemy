using Barnamenevisan.Localizing.Generator;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Security;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Statics;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Events.User;
using Kidemy.Domain.Interfaces.User;
using Kidemy.Domain.Models.User;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PARSGREEN.RESTful.SMS;
using User = Kidemy.Domain.Models.User.User;

namespace Kidemy.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly ISmsSenderSevice _smsSender;
        private readonly IAccountCodeService _accountService;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;
        private readonly IViewRenderService _viewRenderService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICacheManager _cacheManager;
        private readonly IStringLocalizer _localizer;
        private readonly IAccountCodeService _accountCodeService;
        private readonly IVIPMembersService _vIPMembersService;

        #endregion

        #region Constructor

        public UserService(IUserRepository repository, ILogger<UserService>
                logger, ISmsSenderSevice smsSender, IEmailService
                emailService, IRoleService roleService, IViewRenderService viewRenderService, IAccountCodeService accountService,
            IMediatorHandler mediatorHandler, ICacheManager cacheManager, IStringLocalizer localizer, IAccountCodeService accountCodeService, IVIPMembersService vIPMembersService)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _smsSender = smsSender;
            _roleService = roleService;
            _emailService = emailService;
            _accountService = accountService;
            _userRepository = repository;
            _mediatorHandler = mediatorHandler;
            _cacheManager = cacheManager;
            _localizer = localizer;
            _accountCodeService = accountCodeService;
            _vIPMembersService = vIPMembersService;
        }
        #endregion

        #region Methods

        public async Task<Result<FilterUsersViewModel>> FilterUsersAsync(FilterUsersViewModel model)
        {
            if (model == null) return Result.Failure<FilterUsersViewModel>(ErrorMessages.ProcessFailedError);

            var filterConditions = Filter.GenerateConditions<User>();

            // Status filter
            if (model.Status.HasValue)
            {
                switch (model.Status)
                {
                    case UserStatus.IsBan:
                        filterConditions.Add(u => u.IsBan == true);
                        break;
                    case UserStatus.Active:
                        filterConditions.Add(u => u.IsMobileActive == true);
                        break;
                    case UserStatus.Inactive:
                        filterConditions.Add(u => u.IsMobileActive == false);
                        break;
                }
            }

            // FirstName filter
            if (!string.IsNullOrWhiteSpace(model.FirstName))
            {
                filterConditions.Add(u => u.FirstName.Contains(model.FirstName));
            }

            // LastName filter
            if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                filterConditions.Add(u => u.LastName.Contains(model.LastName));
            }

            // Email filter
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filterConditions.Add(u => u.Email.Contains(model.Email));
            }

            // Mobile filter
            if (!string.IsNullOrWhiteSpace(model.Mobile))
            {
                filterConditions.Add(u => u.Mobile.Contains(model.Mobile));
            }

            // Roles filter
            if (model.RoleId is not null)
            {
                filterConditions.Add(user => user.Roles.Any(userRole => userRole.RoleId == model.RoleId));
            }

            //Filter IsMobileAvtice
            if (model.IsMobileActive != null)
            {
                filterConditions.Add(x => x.IsMobileActive == model.IsMobileActive);
            }

            //Filter IsMobileExist
            if (model.IsMobileExist != null)
            {
                if (model.IsMobileExist == true)
                {
                    filterConditions.Add(x => x.Mobile != null);
                }
                else
                {
                    filterConditions.Add(x => x.Mobile == null);
                }
            }

            //Filter IsEmailExist
            if (model.IsEmailExist != null)
            {
                if (model.IsEmailExist == true)
                {
                    filterConditions.Add(x => x.Email != null);
                }
                else
                {
                    filterConditions.Add(x => x.Email == null);
                }
            }

            if (model.UserId is not null)
            {
                filterConditions.Add(u => u.Id == model.UserId);
            }

            await _userRepository.FilterAsync(model, filterConditions, UserMapper.MapUserViewModel);

            return model;
        }

        public async Task<Result<AdminSideUpsertUserViewModel>> GetUserForEditInAdminSideByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUpsertUserViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync(id, includeProperties: nameof(User.Roles));

            if (user == null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure<AdminSideUpsertUserViewModel>(ErrorMessages.ProcessFailedError);
            }

            var model = new AdminSideUpsertUserViewModel().MapFrom(user);

            return model;
        }

        public async Task<Result<ClientSideUpdateUserViewModel>> GetUserProfileForEditByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<ClientSideUpdateUserViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure<ClientSideUpdateUserViewModel>(ErrorMessages.ProcessFailedError);
            }

            var model = new ClientSideUpdateUserViewModel().MapFrom(user);

            return model;
        }

        public async Task<Result<string?>> GetUserEmailByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<string?>(ErrorMessages.ProcessFailedError);

            var key = string.Format(CacheKeys.UserEmailByUserIdCacheKey, id);

            return await _cacheManager.GetAsync(key, async () =>
            {
                var email = await _userRepository.GetUserEmailByIdAsync(id);

                return email;
            });
        }

        public async Task<Result<string>> GetUserMobileByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<string>(ErrorMessages.ProcessFailedError);

            var key = string.Format(CacheKeys.UserMobileByUserIdCacheKey, id);

            return await _cacheManager.GetAsync(key, async () =>
            {
                var mobile = await _userRepository.GetUserMobileByIdAsync(id);

                return mobile;
            });
        }

        public async Task<Result> ConfirmUserMobileAsync(ConfirmMobileViewModel model)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Mobile == model.Mobile);

            if (user is null)
            {
                _logger.LogError("UserService: ConfirmUserMobileAsync: Not found user by mobile : " + model.Mobile);
                return Result.Failure(ErrorMessages.UserNotFoundByMobileError);
            }

            var validationResult = await _accountCodeService.ValidateCodeAsync(user.Mobile, model.ConfirmationCode);

            if (validationResult.IsFailure) return Result.Failure(validationResult.Message);

            if (!user.IsMobileActive)
            {
                user.IsMobileActive = true;
                user.UpdatedDateOnUtc = DateTime.UtcNow;

                _userRepository.Update(user);
                await _userRepository.SaveAsync();

                await _mediatorHandler.PublishEvent(new UserMobileActivatedEvent(user.Id, user.Mobile));
            }

            return Result.Success();
        }

        public async Task<Result> ConfirmUserEmailAsync(string confirmationCode)
        {
            var receiverResult = await _accountCodeService.GetReceiverByCodeAsync(confirmationCode);

            if (receiverResult.IsFailure) return Result.Failure(receiverResult.Message);

            var email = receiverResult.Value;

            var validationResult = await _accountCodeService.ValidateCodeAsync(email, confirmationCode);

            if (validationResult.IsFailure) return Result.Failure(validationResult.Message);

            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                _logger.LogError("UserService: ConfirmUserEmailAsync: Not found user by email : " + email);
                return Result.Failure(ErrorMessages.UserNotFoundByMobileError);
            }

            if (!user.IsEmailActive)
            {
                user.IsEmailActive = true;
                user.UpdatedDateOnUtc = DateTime.UtcNow;

                _userRepository.Update(user);
                await _userRepository.SaveAsync();

                await _mediatorHandler.PublishEvent(new UserEmailActivatedEvent(user.Id, user.Email));
            }

            return Result.Success();
        }

        public async Task<Result<string?>> GetUserAvatarNameByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<string?>(ErrorMessages.ProcessFailedError);

            var key = string.Format(CacheKeys.AvatarNameByUserIdCacheKey, id);

            return await _cacheManager.GetAsync(key, async () =>
            {
                var avatarName = await _userRepository.GetUserAvatarNameByIdAsync(id);

                return avatarName;
            });
        }

        public async Task<Result<ClientSideUserViewModel>> GetUserByMobileAsync(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return Result.Failure<ClientSideUserViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.FirstOrDefaultAsync(u => u.Mobile == mobile);

            if (user == null)
            {
                _logger.LogError($"Ther is no user with mobile : {mobile}");
                return Result.Failure<ClientSideUserViewModel>(ErrorMessages.UserNotFoundByMobileError);
            }
            var model = new ClientSideUserViewModel().MapFrom(user);

            return model;
        }

        public async Task<Result<ClientSideUserViewModel>> GetUserByMobileAsync(int id)
        {
            if (id <= 0) return Result.Failure<ClientSideUserViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure<ClientSideUserViewModel>(ErrorMessages.UserNotFoundByMobileError);
            }
            var model = new ClientSideUserViewModel().MapFrom(user);

            return model;
        }
        public async Task<Result> UpdateUserByAdminAsync(AdminSideUpsertUserViewModel model)
        {
            if (model == null) return Result.Failure<AdminSideUpsertUserViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync(model.Id, includeProperties: nameof(User.Roles));

            var copiedUser = user?.DeepCopy<User>();

            if (user == null)
            {
                _logger.LogError($"Ther is no user with id : {model.Id}");
                return Result.Failure<AdminSideUpsertUserViewModel>(ErrorMessages.ProcessFailedError);
            }

            // validate email
            var result = await ValidateEmail(model.Email, user.Id);
            if (result.IsFailure) return result;

            // validate mobile
            result = await ValidateMobile(model.Mobile, user.Id);
            if (result.IsFailure) return result;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.Mobile = model.Mobile;
            user.BirthDateOnUtc = model.BirthDate?.ConvertToEnglishNumber().ParseUserDateToUTC();
            user.IsMobileActive = model.IsMobileActive;
            user.IsEmailActive = model.IsEmailActive;
            user.IsBan = model.IsBan;
            user.UpdatedDateOnUtc = DateTime.UtcNow;

            // change password
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.Password = PasswordHelper.EncodePasswordSHA256(model.Password);
            }

            // update roles
            UpdateRolesOfUser(user, model.RoleIds);

            if (model.AvatarFile != null) ChangeAvatar(user, model.AvatarFile);

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userUpdatedEvent = new UserUpdatedByAdminEvent(
                user.Id,
                user.Email,
                copiedUser.Email,
                user.Mobile,
                copiedUser.Mobile,
                user.FirstName,
                copiedUser.FirstName,
                user.LastName,
                copiedUser.LastName,
                user.Password,
                copiedUser.Password,
                user.IsEmailActive,
                copiedUser.IsEmailActive,
                user.IsMobileActive,
                copiedUser.IsMobileActive,
                user.IsBan,
                copiedUser.IsBan,
                user.BirthDateOnUtc,
                copiedUser.BirthDateOnUtc,
                user.Gender,
                copiedUser.Gender,
                user.Roles.Select(r => r.RoleId).ToList(),
                copiedUser.Roles.Select(r => r.RoleId).ToList());

            await _mediatorHandler.PublishEvent(userUpdatedEvent);

            return Result.Success();
        }

        public async Task<Result> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                return Result.Failure(ErrorMessages.NullValue);
            }

            // validate mobile
            var result = await ValidateMobile(model.Mobile);
            if (result.IsFailure) return result;

            // validate password
            var passwordValidationResult = model.Password.PasswordIsValid(
                minLength: 8,
                requiredLowerCase: false,
                requiredUpperCase: false
            );

            if (passwordValidationResult.IsFailure)
            {
                var message = string.Format(_localizer[passwordValidationResult.Message].ToString(), 8);
                return Result.Failure(message);
            }

            var toCreateUser = new User();
            toCreateUser.Mobile = model.Mobile;
            toCreateUser.Password = model.Password.EncodePasswordSHA256();

            await _userRepository.InsertAsync(toCreateUser);
            await _userRepository.SaveAsync();

            var @event = new UserRegisteredEvent(
                            toCreateUser.Id,
                            toCreateUser.Mobile,
                            toCreateUser.Password);

            await _mediatorHandler.PublishEvent(@event);

            // send confirm code
            result = await SendMobileConfirmationCodeAsync(toCreateUser.Mobile);

            return result;
        }

        public async Task<Result> UpdateUserProfileAsync(ClientSideUpdateUserViewModel model)
        {
            var message = SuccessMessages.SuccessfullyDone;

            if (model == null) return Result.Failure(ErrorMessages.ProcessFailedError);

            User? user = await _userRepository.GetByIdAsync(model.Id);

            if (user == null)
            {
                _logger.LogError($"Ther is no user with id : {model.Id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var copiedUser = user?.DeepCopy<User>();

            // validate email
            if (!string.IsNullOrWhiteSpace(model.Email) && user.Email != model.Email)
            {
                var result = await ValidateEmail(model.Email, user.Id);
                if (result.IsFailure) return result;

                user.IsEmailActive = false;
                user.Email = model.Email;
            }

            if (!user.IsEmailActive && !string.IsNullOrWhiteSpace(user.Email))
            {
                var result = await _accountService.GenerateAccountCodeAsync(user.Email);

                if (result.IsFailure)
                    return Result.Failure(result.Message);
                var emailConfirmCode = result.Value;

                string emailActivationText = $"{_localizer["To confirm your email click on the link"]}: {Environment.NewLine}{SiteTools.SiteAddress}/activate-email/{emailConfirmCode}";

                string emailBody = await _viewRenderService.RenderToStringAsync("Email/EmailNotification", emailActivationText);

                if (emailBody is not null)
                {
                    bool isSendingSuccess = await _emailService.SendEmailAsync(user.Email, _localizer["EmailConfirmation"].ToString(), emailBody);

                    if (!isSendingSuccess) return Result.Failure(ErrorMessages.ActivationEmailWasNotSentError);
                }

                message = SuccessMessages.SendActivationEmailLinkSuccessfully;
            }

            ChangeAvatar(user, model.Avatar);

            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.Gender = model.Gender;
            user.BirthDateOnUtc = model.BirthDate?.ConvertToEnglishNumber().ParseUserDateToUTC();
            user.UpdatedDateOnUtc = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userUpdatedEvent = new UserProfileUpdatedByUserEvent(
                user.Id,
                user.Email,
                copiedUser.Email,
                user.FirstName,
                copiedUser.FirstName,
                user.LastName,
                copiedUser.LastName,
                user.IsEmailActive,
                copiedUser.IsEmailActive,
                user.BirthDateOnUtc,
                copiedUser.BirthDateOnUtc,
                user.Gender,
                copiedUser.Gender);

            await _mediatorHandler.PublishEvent(userUpdatedEvent);


            return Result.Success(message);
        }

        public async Task<Result> DeleteUserByAdminAsync(int? id)
        {
            if (id is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync((int)id);

            if (user is null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            user.IsDeleted = true;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userDeletedEvent =
                new UserDeletedEvent
                (
                    user.Id
                );
            await _mediatorHandler.PublishEvent(userDeletedEvent);

            return Result.Success();
        }

        #region Unable and able to withdraw
        public async Task<Result> UnableToWithdrawUser(int? id)
        {
            if (id is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync((int)id);

            if (user is null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            user.UnableToWithdraw = true;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userDeletedEvent =
                new UserUnabledToWithdrawEvent
                (
                    user.Id
                );
            await _mediatorHandler.PublishEvent(userDeletedEvent);

            return Result.Success();
        }

        public async Task<Result> AbleToWithdrawUser(int? id)
        {
            if (id is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync((int)id);

            if (user is null)
            {
                _logger.LogError($"Ther is no user with id : {id}");
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            user.UnableToWithdraw = false;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userDeletedEvent =
                new UserUnabledToWithdrawEvent
                (
                    user.Id
                );
            await _mediatorHandler.PublishEvent(userDeletedEvent);

            return Result.Success();
        }
        public async Task<Result<bool>> MasterHasAccessToWithraw(int? id)
        {
            var user = await _userRepository.GetByIdAsync(id.Value);

            if (user is null) return Result.Failure<bool>(ErrorMessages.NullValue);

            if (user.UnableToWithdraw)
                return true;

            return false;
        }

        #endregion

        public async Task<Result> CreateUserAsync(AdminSideUpsertUserViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.ProcessFailedError);

            // validate email
            var result = await ValidateEmail(model.Email);
            if (result.IsFailure) return result;

            // validate mobile
            result = await ValidateMobile(model.Mobile);
            if (result.IsFailure) return result;

            var toCreateUser = new User();
            toCreateUser.FirstName = model.FirstName;
            toCreateUser.LastName = model.LastName;
            toCreateUser.Email = model.Email;
            toCreateUser.Mobile = model.Mobile;
            toCreateUser.Gender = model.Gender;
            toCreateUser.IsMobileActive = model.IsMobileActive;
            toCreateUser.IsEmailActive = model.IsEmailActive;
            toCreateUser.BirthDateOnUtc = model.BirthDate?.ConvertToEnglishNumber().ParseUserDateToUTC();

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                toCreateUser.Password = model.Password.EncodePasswordSHA256();
            }

            // update roles
            UpdateRolesOfUser(toCreateUser, model.RoleIds);

            if (model.AvatarFile != null) ChangeAvatar(toCreateUser, model.AvatarFile);
            else toCreateUser.AvatarName = SiteTools.DefaultImageName;

            await _userRepository.InsertAsync(toCreateUser);
            await _userRepository.SaveAsync();

            var userCreatedEvent =
                new UserCreatedEvent
                    (
                        toCreateUser.Id,
                        toCreateUser.Email,
                        toCreateUser.Mobile,
                        toCreateUser.FirstName,
                        toCreateUser.LastName,
                        toCreateUser.Password,
                        toCreateUser.IsEmailActive,
                        toCreateUser.IsMobileActive,
                        toCreateUser.IsBan,
                        toCreateUser.BirthDateOnUtc,
                        toCreateUser.Gender,
                        toCreateUser.Roles.Select(r => r.RoleId).ToList()
                    );

            await _mediatorHandler.PublishEvent(userCreatedEvent);
            return Result.Success();
        }

        public async Task<Result<AdminSideUserDetailsViewModel>> GetUserInfoAsync(int id)
        {
            if (id <= 0) return Result.Failure<AdminSideUserDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null) return Result.Failure<AdminSideUserDetailsViewModel>(ErrorMessages.ProcessFailedError);


            return new AdminSideUserDetailsViewModel().MapFrom(user);
        }

        public async Task<int> GetUserCount()
        {
            return await _userRepository.GetUserCount();
        }

        public async Task<Result> SendMobileConfirmationCodeAsync(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return Result.Failure(ErrorMessages.MobileIsRequiredError);

            var accountCode = await _accountCodeService.GenerateAccountCodeAsync(mobile);

            if (accountCode.IsFailure) return Result.Failure(accountCode.Message);

            var result = await _smsSender.SendSms(mobile, _localizer["Your confirm code: "].ToString() + accountCode.Value);

            return result;
        }

        public async Task<Result<bool>> ExistsUser(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return Result.Failure<bool>(ErrorMessages.MobileIsRequiredError);

            var isExists = await _userRepository.AnyAsync(u => u.Mobile == mobile);
            
            return isExists;
        }

        public async Task<Result<ClientSideUserViewModel>> ConfirmLogin(LoginViewModel model)
        {
            var encodedPassword = model.Password.EncodePasswordSHA256();
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Mobile == model.Mobile && u.Password == encodedPassword);

            if (user is null)
            {
                return Result.Failure<ClientSideUserViewModel>(ErrorMessages.InformationAreNotValidError);
            }

            if (user.IsBan)
            {
                return Result.Failure<ClientSideUserViewModel>(ErrorMessages.YouAreBannedError);
            }

            if (!user.IsMobileActive)
            {
                await SendMobileConfirmationCodeAsync(model.Mobile);
            }

            var result = new ClientSideUserViewModel().MapFrom(user);

            return result;
        }

        public async Task<Result> ResetPassword(string mobile, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(mobile)) return Result.Failure(ErrorMessages.MobileIsRequiredError);

            if (string.IsNullOrWhiteSpace(newPassword)) return Result.Failure(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.FirstOrDefaultAsync(u => u.Mobile == mobile);

            var copiedUser = user?.DeepCopy<User>();

            if (user is null)
                return Result.Failure(ErrorMessages.FailedOperationError);

            var passwordValidationResult = newPassword.PasswordIsValid(
                                        minLength: 8,
                                        requiredLowerCase: false,
                                        requiredUpperCase: false);

            if (passwordValidationResult.IsFailure)
            {
                var message = string.Format(_localizer[passwordValidationResult.Message].ToString(), 8);
                return Result.Failure(message);
            }

            user.Password = newPassword.EncodePasswordSHA256();
            user.UpdatedDateOnUtc = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userUpdatedPasswordEvent = new UserPasswordResetedEvent(
                    user.Id,
                    copiedUser.Password,
                    newPassword);

            await _mediatorHandler.PublishEvent(userUpdatedPasswordEvent);

            return Result.Success();
        }

        public async Task<Result> UpdateUserPassword(int userid, ChangePasswordViewModel model)
        {
            if (userid <= 0 || model is null)
            {
                return Result.Failure(ErrorMessages.ProcessFailedError);
            }

            var user = await _userRepository.GetByIdAsync(userid);

            var copiedUser = user?.DeepCopy<User>();

            if (user is null)
            {
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            if (user.Password != model.OldPassword.EncodePasswordSHA256())
            {
                return Result.Failure(ErrorMessages.OldPasswordNotValidError);
            }

            var passwordValidationResult = model.NewPassword.PasswordIsValid(
                                minLength: 8,
                                requiredLowerCase: false,
                                requiredUpperCase: false);

            if (passwordValidationResult.IsFailure)
            {
                var message = string.Format(_localizer[passwordValidationResult.Message].ToString(), 8);
                return Result.Failure(message);
            }

            user.Password = model.NewPassword.EncodePasswordSHA256();
            user.UpdatedDateOnUtc = DateTime.UtcNow;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            var userUpdatedPasswordEvent =
                new UserPasswordUpdatedEvent
                (
                    userid,
                    copiedUser.Password,
                    user.Password
                );

            await _mediatorHandler.PublishEvent(userUpdatedPasswordEvent);

            return Result.Success();
        }

        public async Task<Result<bool>> IsUserBan(int userId)
        {
            if (userId <= 0) return Result.Failure<bool>(ErrorMessages.ProcessFailedError);

            var user = await _userRepository.GetByIdAsync(userId);

            return user?.IsBan ?? false;
        }

        public async Task<List<UserFullNameModel>> GetUsersFullNames(List<int> userIds)
        {
            return await _userRepository.GetUsersFullNames(userIds);
        }

        public async Task<List<UserNameAndUserProfileModel>> GetUsersProfileDetails(List<int> userIds)
        {
            return await _userRepository.GetUserNamesAndUserProfilesById(userIds);
        }

        public async Task<Result<string>> GetUserFullName(int id)
        {
            var key = string.Format(CacheKeys.UserNameByUserIdCacheKey, id);
            return await _cacheManager.GetAsync(key, async () =>
            {
                var fullName = await _userRepository.GetUserFullName(id);

                return fullName;
            });
        }

        public async Task<Result<int>> GetMastersCountAsync()
        {
            return await _userRepository.GetMastersCountAsync();
        }

        public async Task<Result<int>> GetTodayUserCountAsync() 
            => await _userRepository.GetUserCount(user => user.CreatedDateOnUtc >= DateTime.Today);
         
        #endregion

        #region Utilities

        private void UpdateRolesOfUser(User user, List<int> expectedRoleIds)
        {
            var rolesHaveChanged = user.Roles?.Count != expectedRoleIds?.Count ||
                        !(user.Roles?.All(userRole => expectedRoleIds?.Contains(userRole.RoleId) ?? false) ?? false);

            if (rolesHaveChanged)
            {
                // delete roles
                foreach (var userRole in user.Roles?.Where(role => !(expectedRoleIds?.Contains(role.RoleId) ?? false))?.ToList() ?? new List<UserRoleMapping>())
                {
                    user.Roles.Remove(userRole);
                }

                // add new roles
                if (expectedRoleIds?.Count == 0) return;

                foreach (var roleId in expectedRoleIds?.Where(roleId => user?.Roles?.All(role => role.RoleId != roleId) ?? true)?.ToList() ?? new List<int>())
                {
                    user.Roles.Add(new UserRoleMapping
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                        User = user
                    });
                }
            }
        }

        private async Task<Result> ValidateEmail(string email, int? userId = null)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var existUsersWithSameEmail = await _userRepository
                    .AnyAsync(filter: u => u.Email == email && u.Id != userId);


                if (existUsersWithSameEmail)
                {
                    return Result.Failure(ErrorMessages.DuplicateEmailError);
                }
            }

            return Result.Success();
        }

        private async Task<Result> ValidateMobile(string mobile, int? userId = null)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return Result.Failure(ErrorMessages.MobileIsRequiredError);
            }

            if (!CommonTools.IsMobile(mobile))
            {
                return Result.Failure(ErrorMessages.MobileFormatError);
            }

            var existUserWithSameMobile = await _userRepository
                .AnyAsync(filter: u => u.Mobile == mobile && u.Id != userId);

            if (existUserWithSameMobile)
            {
                return Result.Failure(ErrorMessages.DuplicateMobileError);
            }

            return Result.Success();
        }

        private void ChangeAvatar(User user, IFormFile avatarFile)
        {
            if (avatarFile != null && avatarFile.IsImage())
            {
                var newAvatarName = Guid.NewGuid() + Path.GetExtension(avatarFile.FileName);

                if (user.AvatarName != SiteTools.DefaultImageName)
                {
                    avatarFile.AddImageToServer(newAvatarName, SiteTools.UserImagePath, 400, 300,
                    SiteTools.UserImageThumbPath, user.AvatarName);
                }
                else
                {
                    avatarFile.AddImageToServer(newAvatarName, SiteTools.UserImagePath, 400, 300,
                    SiteTools.UserImageThumbPath);
                }

                user.AvatarName = newAvatarName;
            }
        }

        #endregion


        public async Task<Result<List<int>>> GetUsersIds()
        {
            return await _userRepository.GetUsersIds();
        }

        public async Task<Result<List<string>>> GetUsersEmails()
        {
            return await _userRepository.GetUsersEmails();
        }

        public async Task<Result<List<string>>> GetUsersNumbers()
        {
            return await _userRepository.GetUsersMobiles();
        }
        public async Task<Result<List<ClientSideUserNameAndUserProfileViewModel>>> GetUserNameAndUserProfileByUserId(List<int> id)
        {
            if (id.Count() < 1) return Result.Failure<List<ClientSideUserNameAndUserProfileViewModel>>(ErrorMessages.FailedOperationError);
            var usersInfo = await _userRepository.GetUserNamesAndUserProfilesById(id);

            var model = usersInfo.Select(user => new ClientSideUserNameAndUserProfileViewModel().MapFrom(user)).ToList();

            return model;
        }

        public async Task<Result<bool>> UserIsMasterAsync(int userId)
        {
            if (userId <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var user = await _userRepository.GetByIdAsync(userId, includeProperties: nameof(User.Roles));

            var result = user?.Roles?.Any(userRole => userRole.RoleId == Roles.Master) ?? false;

            return result;
        }

        public async Task<Result<bool>> UserIsAdminAsync(int userId)
        {
            if (userId <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var user = await _userRepository.GetByIdAsync(userId, includeProperties: nameof(User.Roles));

            var result = user?.Roles?.Any(userRole => userRole.RoleId == Roles.Admin) ?? false;

            return result;
        }

        public async Task<Result<List<AdminSideUserNameAndMobileForMasterModel>>> GetUserNameAndMobileForMasterByUserId(List<int> id)
        {
            if (id.Count() < 1) return Result.Failure<List<AdminSideUserNameAndMobileForMasterModel>>(ErrorMessages.FailedOperationError);
            return await _userRepository.GetUserNameAndMobileForMasterByUserId(id);

        }

        public async Task<Result<bool>> IsUserVIPMemberAsync(int userId)
        {
            if (userId <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.ProcessFailedError);
            }

            var userIsVIPMember = await _vIPMembersService.IsUserVIPMemberAsync(userId);

            if (!userIsVIPMember.Value) return false;

            return true;



        }

        public async Task<Result<List<UserFullNameModel>>> GetMastersListAsync()
        {
            return await _userRepository.GetUsersThatHaveMasterRoleList();

        }

        public async Task<Result<List<User>>> GetUserIdByuserName(string userName)
        {
            var users = await _userRepository.GetAllAsync(x => (x.FirstName + x.LastName).Contains(userName) && !x.IsBan && !x.IsDeleted);
            if (users != null && users.Any())
                return users;

            return Result.Failure<List<User>>(ErrorMessages.NullValue);
        }

    }
}
