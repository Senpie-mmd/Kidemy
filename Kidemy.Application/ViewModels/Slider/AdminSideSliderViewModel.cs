using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Slider;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Slider
{
    public class AdminSideSliderViewModel : BaseEntityViewModel<int>
    {
        public AdminSideSliderViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ImageName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }
        public SliderPlace SliderPlace { get; set; }
        public string? URL { get; set; }
        public bool IsPublished { get; set; }
    }
}
