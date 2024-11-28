using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Blog
{
    public class BlogLinksForClientSideViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Blog))]
        public string Title { get; set; }
        public string slug { get; set; }
    }
}
