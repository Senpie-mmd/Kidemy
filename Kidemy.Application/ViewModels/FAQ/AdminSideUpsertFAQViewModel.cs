using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Link;
using Kidemy.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.FAQ
{
    public class AdminSideUpsertFAQViewModel : LocalizableViewModel<LocalizedAdminSideUpsertFAQViewModel>
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Answer { get; set; }
    }

    public class LocalizedAdminSideUpsertFAQViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Answer")]
        [UIHint("ckeditor")]
        public string? Answer { get; set; }

    }

}
