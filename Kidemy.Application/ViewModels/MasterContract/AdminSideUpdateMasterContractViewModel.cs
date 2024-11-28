using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.MasterContract
{
    public class AdminSideUpdateMasterContractViewModel : LocalizableViewModel<LocalizedAdminSideUpdateMasterContractViewModel>
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "FileName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string? FileName { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
    public class LocalizedAdminSideUpdateMasterContractViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

    }
}
