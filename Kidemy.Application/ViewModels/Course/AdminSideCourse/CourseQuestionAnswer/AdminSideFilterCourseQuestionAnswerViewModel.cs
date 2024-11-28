using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestionAnswer
{
    public class AdminSideFilterCourseQuestionAnswerViewModel : BasePaging<AdminSideCourseQuestionAnswerViewModel>
    {
        public int QuestionId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
