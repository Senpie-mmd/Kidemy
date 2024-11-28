using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Master
{
    public class FilterForAdminSideMasterViewModel : BasePaging<AdminSideMasterViewModel>
    {
        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }

        [Display(Name = "LastName")]
        public string? LastName { get; set; }

        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }

        [Display(Name = "NationalIDNumber")]
        public string? NationalIDNumber { get; set; }

        public int? UserId { get; set; }


    }
}
