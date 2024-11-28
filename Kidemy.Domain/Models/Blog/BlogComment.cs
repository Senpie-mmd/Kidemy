using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Blog;

namespace Kidemy.Domain.Models.Blog
{
    public class BlogComment : AuditBaseEntity<int>
    {
        public string Message { get; set; }

        public bool IsConfirmed { get; set; }

        public int CommentedById { get; set; }

        public int BlogId { get; set; }

        public int? ReplyCommentId { get; set; }

        #region Relations

        public Blog Blog { get; set; }

        public ICollection<BlogComment> ReplyComments { get; set; }

        // Navigation property for the parent comment
        public BlogComment ParentComment { get; set; }
        #endregion
    }

}
