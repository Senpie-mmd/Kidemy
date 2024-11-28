using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Blog
{
    public class AdminSideBlogViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Domain.Models.Blog.Blog))]
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public string ImageName { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorAvatar{ get; set;}
    }
}
