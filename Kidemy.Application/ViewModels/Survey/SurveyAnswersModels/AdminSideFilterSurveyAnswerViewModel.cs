using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Survey.SurveyAnswersModels
{
    public class AdminSideFilterSurveyAnswerViewModel : BasePaging<AdminSideSurveyAnswerViewModel>
    {
        [Display(Name = "User")]
        public int? UserId { get; set; }

        public string? TempUserName { get; set; }

        [Display(Name = "Survey")]
        public int? SurveyId { get; set; }

        [Display(Name = "FromDate")]
        public string? FromDate { get; set; }

        [Display(Name = "ToDate")]
        public string? ToDate { get; set; }
    }
}
