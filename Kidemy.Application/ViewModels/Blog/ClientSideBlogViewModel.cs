using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Blog
{
    public class ClientSideBlogViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Page))]
        public string Title { get; set; }

        [Translate(EntityName = nameof(Page))]
        public string Body { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public string ImageName { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime CreatedDateOnUTC { get; set; }
        public int CommentCount { get; set; }
    }
}
