using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Ticket;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Models.Course;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICourseQuestionService _courseQuestionService;

        #endregion

        #region Constructor

        public AdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursViewComponent(ICourseQuestionService courseQuestionService)
        {
            _courseQuestionService = courseQuestionService;
        }
        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _courseQuestionService.FilterAdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48HoursAsync(new AdminSideFilterCourseQuestionViewModel
            {
                TakeEntity = 5,
                QuestionState=CourseQuestionState.NotConfirmed,
            });

            return View("AdminDashboardCourseQuestionsDoesNotAnsweredByTeacherAfter48Hours", model.Value);
        }

        #endregion

    }
}
