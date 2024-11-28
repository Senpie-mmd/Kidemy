using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Survey;
using Kidemy.Domain.Models.Survey;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Survey.SurveyQuestionModels
{
    public class ClientSideSurveyQuestionViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = "SurveyQuestion")]
        public string Title { get; set; }

        public int SurveyId { get; set; }

        [Translate(EntityName = nameof(SurveyQuestion))]
        public string? Options { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        public SurveyQuestionType Type { get; set; }
    }
}
