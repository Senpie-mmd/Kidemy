using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.VIPPlan
{
    public class AdminSideUpsertVIPPlanViewModel : LocalizableViewModel<LocalizedAdminSideUpsertVIPPlanViewModel>
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Title { get; set; }

        [Display(Name = "DurationDay")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public int DurationDay { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public decimal Price { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
    public class LocalizedAdminSideUpsertVIPPlanViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }
    }
}
