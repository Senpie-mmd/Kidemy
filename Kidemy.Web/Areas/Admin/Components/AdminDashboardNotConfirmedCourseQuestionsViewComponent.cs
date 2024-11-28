using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Ticket;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Models.Course;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.Admin.Components
{
    public class AdminDashboardNotConfirmedCourseQuestionsViewComponent : ViewComponent
    {
        #region Fields

        private readonly ICourseQuestionService _courseQuestionService;

        #endregion

        #region Constructor

        public AdminDashboardNotConfirmedCourseQuestionsViewComponent(ICourseQuestionService courseQuestionService)
        {
            _courseQuestionService = courseQuestionService;
        }
        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _courseQuestionService.FilterNotConfirmedAsync(new AdminSideFilterCourseQuestionViewModel
            {
                TakeEntity = 5,
            });

            return View("AdminDashboardNotConfirmedCourseQuestions", model.Value);
        }

        #endregion

    }
}
