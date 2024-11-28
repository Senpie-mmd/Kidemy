using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Course;

namespace Kidemy.Domain.DTOs.Course
{
    public class CourseDetailsModel
    {
        public int Id { get; set; }
        public string ImageFileName { get; set; }
        public string DemoVideoFileName { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int VideoCount { get; set; }
        public List<TimeSpan> CourseVideosTotalTime { get; set; }
        public bool HasCertificate { get; set; }
        public List<CourseTagsModel> Tags { get; set; }
        public string Slug { get; set; }
        public CourseType Type { get; set; }
        public decimal? Price { get; set; }
        public List<CourseCommentsScore> CourseScore { get; set; }
        public int MasterId { get; set; }
    }
}
