using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Interfaces.User;
using Kidemy.Domain.Models.Identity;
using Kidemy.Domain.Models.User;
using Kidemy.Domain.Statics;
using Kidemy.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace Kidemy.Infra.Data.Repositories.User
{
    public class UserRepository : EfRepository<Kidemy.Domain.Models.User.User, int>, IUserRepository
    {
        public UserRepository(KidemyContext context) : base(context)
        {
        }

        public Task<string?> GetUserEmailByIdAsync(int id)
        {
            return _dbSet.Where(u => u.Id == id).Select(u => u.Email).FirstOrDefaultAsync();
        }
        public Task<string?> GetUserAvatarNameByIdAsync(int id)
        {
            return _dbSet.Where(u => u.Id == id).Select(u => u.AvatarName).FirstOrDefaultAsync();
        }

        public Task<string?> GetUserMobileByIdAsync(int id)
        {
            return _dbSet.Where(u => u.Id == id).Select(u => u.Mobile).FirstOrDefaultAsync();
        }

        public async Task<List<UserFullNameModel>> GetUsersFullNames(List<int> userIds)
        {
            return await _context.Set<Domain.Models.User.User>()
                .Where(u => userIds.Contains(u.Id)).Select(u => new UserFullNameModel()
                {
                    UserId = u.Id,
                    UserFullName = (!string.IsNullOrWhiteSpace(u.FirstName) || !string.IsNullOrWhiteSpace(u.LastName))
                        ? $"{u.FirstName} {u.LastName}"
                        : u.Mobile ?? "-"
                }).ToListAsync();


        }

        public Task<string> GetUserFullName(int userId)
        {
            return _dbSet.Where(u => u.Id == userId)
                .Select(u => !string.IsNullOrWhiteSpace(u.FirstName) || !string.IsNullOrWhiteSpace(u.LastName)
                        ? $"{u.FirstName} {u.LastName}"
                        : u.Mobile ?? "-"
                ).FirstOrDefaultAsync();
        }

        public Task<int> GetUserCount(Expression<Func<Domain.Models.User.User, bool>>? expression = null)
        {
            return expression == null ? _dbSet.CountAsync() : _dbSet.CountAsync(expression);
        }

        public async Task<List<string>> GetUsersEmails()
        {
            return await _dbSet.Select(n => n.Email).ToListAsync();
        }

        public async Task<List<int>> GetUsersIds()
        {
            return await _dbSet.Select(n => n.Id).ToListAsync();
        }

        public async Task<List<string>> GetUsersMobiles()
        {

            return await _dbSet.Select(n => n.Mobile).ToListAsync();
        }

        public Task<List<UserNameAndUserProfileModel>> GetUserNamesAndUserProfilesById(List<int> id)
        {
            IQueryable<Domain.Models.User.User> query = _dbSet
                .AsNoTracking()
                .AsQueryable();

            query = query.Where(n => id.Contains(n.Id));

            return query.Select(User => new UserNameAndUserProfileModel
            {
                UserId = User.Id,
                UserName = (User.FirstName != null && User.LastName != null) ? User.FirstName + " " + User.LastName : User.Mobile.Substring(0, 4) + "***" + User.Mobile.Substring(7),
                UserAvatar = User.AvatarName,
                Mobile = User.Mobile
            }).ToListAsync();
        }

        public Task<List<AdminSideUserNameAndMobileForMasterModel>> GetUserNameAndMobileForMasterByUserId(List<int> id)
        {
            IQueryable<Domain.Models.User.User> query = _dbSet
            .AsNoTracking()
            .AsQueryable();

            query = query.Where(n => id.Contains(n.Id));

            return query.Select(user => new AdminSideUserNameAndMobileForMasterModel
            {
                Id = user.Id,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                UserMobile = user.Mobile
            }).ToListAsync();
        }

        public async Task<List<UserFullNameModel>> GetUsersThatHaveMasterRoleList()
        {
            return await _dbSet
                .Where(u => u.Roles.Any(r => r.RoleId == Roles.Master)).Select(u => new UserFullNameModel()
                {
                    UserId = u.Id,
                    UserFullName = (!string.IsNullOrWhiteSpace(u.FirstName) || !string.IsNullOrWhiteSpace(u.LastName))
                        ? $"{u.FirstName} {u.LastName}"
                        : u.Mobile ?? "-"
                }).ToListAsync();

        }

        public Task<int> GetMastersCountAsync()
        {
            return _dbSet.Where(u => u.Roles.Any(r => r.RoleId == Roles.Master)).CountAsync();
        }
    }
}
