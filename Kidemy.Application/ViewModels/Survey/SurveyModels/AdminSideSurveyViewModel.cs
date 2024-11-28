using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Survey.SurveyModels
{
    public class AdminSideSurveyViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = "Survey")]
        public string Title { get; set; }

        public bool IsPublished { get; set; }
    }
}
