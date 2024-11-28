using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.DynamicText;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.DynamicText
{
    public class FilterDynamicTextViewModel : BasePaging<AdminSideDynamicTextViewModel>
    {
        [Display(Name = "DynamicTextPosition")]
        public DynamicTextPosition? Position { get; set; }
    }
}
