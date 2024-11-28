using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Blog;
using Kidemy.Domain.Models.Course;

namespace Kidemy.Domain.Models.Blog
{
    public class Blog : AuditBaseEntity<int>
    {
        #region Properties

        public string Title { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public string ImageName { get; set; }
        public int AuthorId { get; set; }

        #endregion

        #region Relation

        public ICollection<BlogComment> Comments { get; set; }
        public ICollection<BlogTagMapping> BlogTags { get; set; }

        #endregion
    }
}
