using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Banner;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Banner
{
    public class AdminSideBannerViewModel : BaseEntityViewModel<int>
    {
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
        public BannerPlace Place { get; set; }
        public string? URL { get; set; }
        public bool IsPublished { get; set; }
    }
}
