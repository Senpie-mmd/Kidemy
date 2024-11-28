namespace Kidemy.Domain.Models.Blog
{
    public class BlogTagMapping
    {
        public int BlogId { get; set; }
        public int TagId { get; set; }

        #region Relations
        public Blog Blog { get; set; }
        public BlogTag Tag { get; set; }
        #endregion
    }
}
