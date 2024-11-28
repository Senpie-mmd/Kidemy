using Kidemy.Domain.Enums.Course;

namespace Kidemy.Domain.DTOs.Course
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImageFileName { get; set; }
        public CourseLevel Level { get; set; }
        public string ShortDescription { get; set; }
        public List<int> ScoresList { get; set; }
        public int VideosCount { get; set; }
        public List<TimeSpan> ListOfTimeSpans { get; set; }
        public int Popularity { get; set; }
    }
}
