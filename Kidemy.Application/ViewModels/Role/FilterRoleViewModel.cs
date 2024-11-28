using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Role
{
    public class FilterRoleViewModel : BasePaging<AdminSideRoleViewModel>
    {
        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        [Display(Name = "UniqueName")]
        public string? UniqueName { get; set; }
    }
}
