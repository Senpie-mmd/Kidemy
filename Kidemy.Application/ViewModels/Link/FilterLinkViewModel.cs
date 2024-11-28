using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Enums.Link;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.Link
{
    public class FilterLinkViewModel : BasePaging<AdminSideLinkViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "LinkType")]
        public LinkType? LinkType { get; set; }
    }
}
