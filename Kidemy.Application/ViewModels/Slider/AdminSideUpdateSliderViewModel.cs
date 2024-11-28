using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Slider;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.Slider
{
    public class AdminSideUpdateSliderViewModel : LocalizableViewModel<LocalizedUpdateSliderViewModel>
    {
        public AdminSideUpdateSliderViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(900, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string ImageName { get; set; }

        public IFormFile? Image { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        [Display(Name = "SliderPlace")]
        public SliderPlace SliderPlace { get; set; }

        [Display(Name = "URL")]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? URL { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
    public class LocalizedUpdateSliderViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }

        [Display(Name = "Description")]
        [UIHint("Textarea")]
        [MaxLength(900, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Description { get; set; }

    }
}
