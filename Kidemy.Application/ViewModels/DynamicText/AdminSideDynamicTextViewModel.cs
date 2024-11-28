using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.DynamicText;

namespace Kidemy.Application.ViewModels.DynamicText
{
    public class AdminSideDynamicTextViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(DynamicText))]
        public string Text { get; set; }
        public DynamicTextPosition Position { get; set; }
    }
}
