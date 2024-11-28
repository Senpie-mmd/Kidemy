using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Page;

namespace Kidemy.Application.ViewModels.Page
{
    public class AdminSidePageViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Page))]
        public string Title { get; set; }

        [Translate(EntityName = nameof(Page))]
        public string Body { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public LinkPlace LinkPlace { get; set; }
    }
}
