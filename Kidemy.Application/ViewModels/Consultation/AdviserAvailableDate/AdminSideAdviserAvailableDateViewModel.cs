using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate
{
    public class AdminSideAdviserAvailableDateViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "AdviserId")]
        public int? AdviserId { get; set; }
        [Display(Name = "DayOfWeek")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public DayOfWeek DayOfWeek { get; set; }
        [Display(Name = "StartTime")]
        public TimeSpan? StartTime { get; set; }
        [Display(Name = "EndTime")]
        public TimeSpan? EndTime { get; set; }

    }
}
