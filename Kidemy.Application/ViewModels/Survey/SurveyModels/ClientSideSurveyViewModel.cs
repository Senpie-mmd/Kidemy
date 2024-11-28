using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;

namespace Kidemy.Application.ViewModels.Survey.SurveyModels
{
    public class ClientSideSurveyViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = "Survey")]
        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public List<ClientSideSurveyQuestionViewModel> Questions { get; set; }
    }
}
