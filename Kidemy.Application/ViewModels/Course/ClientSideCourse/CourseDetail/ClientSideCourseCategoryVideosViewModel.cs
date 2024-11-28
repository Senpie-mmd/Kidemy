namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideCourseCategoryVideosViewModel
    {
        public string Title { get; set; }
        public int VideosCount { get; set; }
        public List<ClientSideCourseVideoViewModel> VideosWithDetails { get; set; }
    }
}
