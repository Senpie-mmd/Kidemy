using Kidemy.Application.Convertors;
using Kidemy.Application.ViewModels.User.AdminSide;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Models.User;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class UserMapper
    {
        public static AdminSideUpsertUserViewModel MapFrom(this AdminSideUpsertUserViewModel model, User user)
        {
            model.Id = user.Id;
            model.Email = user.Email;
            model.IsBan = user.IsBan;
            model.Mobile = user.Mobile;
            model.Gender = user.Gender;
            model.Password = user.Password;
            model.LastName = user.LastName;
            model.FirstName = user.FirstName;
            model.AvatarName = user.AvatarName;
            model.IsEmailActive = user.IsEmailActive;
            model.IsMobileActive = user.IsMobileActive;
            model.BirthDate = user.BirthDateOnUtc?.ToUserDate();
            model.RoleIds = user.Roles.Select(role => role.RoleId).ToList();

            return model;
        }

        public static Expression<Func<User, AdminSideUserViewModel>> MapUserViewModel => (User user) => new AdminSideUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Mobile = user.Mobile,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Roles = user.Roles.Select(r => r.RoleId).ToList(),
            Status = user.IsBan
                ? UserStatus.IsBan
                : (user.IsMobileActive ? UserStatus.Active : UserStatus.Inactive)
        };

        public static AdminSideUserDetailsViewModel MapFrom(this AdminSideUserDetailsViewModel model, User user)
        {
            model.Id = user.Id;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Mobile = user.Mobile;
            model.UnableToWidthraw = user.UnableToWithdraw;
            return model;
        }

        public static ClientSideUserViewModel MapFrom(this ClientSideUserViewModel model, User user)
        {
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Mobile = user.Mobile;
            model.Email = user.Email;
            model.AvatarName = user.AvatarName;
            model.IsMobileActive = user.IsMobileActive;
            return model;
        }

        public static ClientSideUpdateUserViewModel MapFrom(this ClientSideUpdateUserViewModel model, User user)
        {
            model.Id = user.Id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Gender = user.Gender;
            model.BirthDate = user.BirthDateOnUtc?.ToUserDate();
            model.Email = user.Email;

            return model;
        }

        public static ClientSideUserNameAndUserProfileViewModel MapFrom(this ClientSideUserNameAndUserProfileViewModel model, UserNameAndUserProfileModel user)
        {
            model.Id = user.UserId;
            model.UserName = user.UserName;
            model.Mobile = user.Mobile;
            model.UserAvatar = user.UserAvatar;

            return model;
        }
        
    }
}
