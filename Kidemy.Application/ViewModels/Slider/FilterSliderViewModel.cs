using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Slider;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Slider
{
    public class FilterSliderViewModel : BasePaging<AdminSideSliderViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Priority")]
        public int? Priority { get; set; }

        [Display(Name = "SliderPlace")]
        public SliderPlace? SliderPlace { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }
    }
}
