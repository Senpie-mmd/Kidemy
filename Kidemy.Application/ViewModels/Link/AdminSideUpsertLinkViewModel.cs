using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Enums.Link;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.Link
{
    public class AdminSideUpsertLinkViewModel : LocalizableViewModel<LocalizedAdminSideUpsertLinkViewModel>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Address { get; set; }

        [Display(Name = "LinkType")]
        public LinkType LinkType { get; set; }
    }

    public class LocalizedAdminSideUpsertLinkViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

    }
}
