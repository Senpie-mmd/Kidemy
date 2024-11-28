using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.FAQ
{
    public class ClientSideFAQViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(FAQ))]
        public string Title { get; set; }

        [Translate(EntityName = nameof(FAQ))]
        public string Answer { get; set; }
    }
}
