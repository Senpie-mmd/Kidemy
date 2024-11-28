using Kidemy.Application.ViewModels.User;
using Kidemy.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Models.Identity;
using Kidemy.Application.ViewModels.Role;

namespace Kidemy.Application.Mapper
{
    public static class RoleMapper
    {
        public static AdminSideUpsertRoleViewModel MapFrom(this AdminSideUpsertRoleViewModel model, Role role)
        {
            model.Id = role.Id;
            model.Title = role.Title;
            model.UniqueName = role.UniqueName;
            model.PermissionsId = role.Permissions?.Select(p => p.PermissionId).ToList();

            return model;
        }

        public static Expression<Func<Role, AdminSideRoleViewModel>> MapRoleViewModel => (role) =>
            new AdminSideRoleViewModel
            {
                Id = role.Id,
                Title = role.Title,
                UniqueName = role.UniqueName
            };

        public static AdminSideRoleViewModel MapFrom(this AdminSideRoleViewModel model, Role role)
        {
            model.Id = role.Id;
            model.Title = role.Title;
            model.UniqueName = role.UniqueName;
            return model;
        }

    }
}
