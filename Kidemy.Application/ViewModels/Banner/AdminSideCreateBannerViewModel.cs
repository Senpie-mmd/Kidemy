using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Banner
{
    public class AdminSideCreateBannerViewModel
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public IFormFile Image { get; set; }

        [Display(Name = "Place")]
        public BannerPlace Place { get; set; }

        [Display(Name = "URL")]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? URL { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
