using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Survey.SurveyModels
{
    public class FilterSurveyViewModel : BasePaging<AdminSideSurveyViewModel>
    {
        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        [Display(Name = "IsPublished")]
        public bool? IsPublished { get; set; }
    }
}
