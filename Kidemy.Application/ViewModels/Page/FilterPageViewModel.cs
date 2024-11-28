using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Page;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Page
{
    public class FilterPageViewModel : BasePaging<AdminSidePageViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }

        [Display(Name = "LinkPlace")]
        public LinkPlace? LinkPlace { get; set; }
    }
}
