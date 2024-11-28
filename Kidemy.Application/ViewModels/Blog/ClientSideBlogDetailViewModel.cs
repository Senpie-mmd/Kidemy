using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;

namespace Kidemy.Application.ViewModels.Blog
{
    public class ClientSideBlogDetailViewModel : BaseEntityViewModel<int>
    {
        public ClientSideBlogDetailViewModel()
        {
            Comments = new List<ClientSideBlogCommentViewModel>();
        }

        [Translate(EntityName = nameof(Page))]
        public string Title { get; set; }

        [Translate(EntityName = nameof(Page))]
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public List<string> Tags { get; set; }
        public string ImageName { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorAvatar { get; set; }
        public DateTime CreatedDateOnUTC { get; set; }

        public List<ClientSideBlogCommentViewModel> Comments { get; set; }
    }
}
