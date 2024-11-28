using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Blog
{
    public class BlogCommentReport : AuditBaseEntity<int>
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public BlogComment Comment { get; set; }
    }

}
