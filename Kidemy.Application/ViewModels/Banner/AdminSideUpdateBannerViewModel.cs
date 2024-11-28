using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Banner;
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

namespace Kidemy.Application.ViewModels.Banner
{
    public class AdminSideUpdateBannerViewModel : BaseEntityViewModel<int>
    {
        public AdminSideUpdateBannerViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        [Display(Name = "Image")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string ImageName { get; set; }
        public IFormFile? Image { get; set; }

        [Display(Name = "Place")]
        public BannerPlace Place { get; set; }

        [Display(Name = "URL")]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? URL { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
