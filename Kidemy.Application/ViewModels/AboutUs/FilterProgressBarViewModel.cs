using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.AboutUs
{
    public class FilterProgressBarViewModel : BasePaging<ProgressBarListViewModel>
    {
        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        [Display(Name = "Percent")]
        public int? Percent { get; set; }
    }
}
