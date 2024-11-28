using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.DynamicText;

namespace Kidemy.Application.ViewModels.DynamicText
{
    public class ClientSideDynamicTextViewModel : BaseEntityViewModel<int>
    {
        public string Text { get; set; }
        public DynamicTextPosition Position { get; set; }
    }
}
