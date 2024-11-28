using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Link;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.FAQ
{
    public class FilterFAQViewModel : BasePaging<AdminSideFAQViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }
    }
}
