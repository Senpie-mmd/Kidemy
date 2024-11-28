using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.InPersonCourse
{
    public class ClientSideFilterInPersonCourseViewModel : BasePaging<ClientSideInPersonCourseViewModel>
    {
        public string? Title { get; set; }
        public string? CourseTag { get; set; }
    }
}
