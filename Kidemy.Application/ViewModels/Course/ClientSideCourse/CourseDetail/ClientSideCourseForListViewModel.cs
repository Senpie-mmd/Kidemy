using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Course;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail
{
    public class ClientSideCourseForListViewModel : BaseEntityViewModel<int>
    {
        public int UserId { get; set; }

        [Display(Name = "CourseTitle")]
        public string Title { get; set; }

        public string Slug { get; set; }

        public DateTime UpdatedDateOnUTC { get; set; }

        public string ImageName { get; set; }

        public CourseType Type { get; set; }
    }
}
