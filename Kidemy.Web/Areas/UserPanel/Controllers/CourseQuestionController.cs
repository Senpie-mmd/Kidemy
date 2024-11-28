using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer;
using Kidemy.Domain.Interfaces.Course;
using Microsoft.AspNetCore.Mvc;

namespace Kidemy.Web.Areas.UserPanel.Controllers
{
    public class CourseQuestionController : BaseUserPanelController
    {
        #region Flieds

        private readonly ICourseQuestionAnswerService  _courseQuestionAnswerService;
        private readonly ICourseService _courseService;
        private readonly ICourseQuestionService _courseQuestionService;

        #endregion

        #region Constructor

        public CourseQuestionController(ICourseQuestionAnswerService courseQuestionAnswerService, ICourseQuestionService courseQuestionService, ICourseService courseService)
        {
            _courseQuestionAnswerService = courseQuestionAnswerService;
            _courseQuestionService = courseQuestionService;
            _courseService = courseService;
        }

        #endregion

        #region Actions


        #region Question List

        [HttpGet]
        public async Task<IActionResult> QuestionsList(ClientSideFilterCourseQuestionViewModel model)
        {
            model.AskedById = User.GetUserId();
            var answers = await _courseQuestionService.FilterCourseQuestionsByUserId(model);

            return View(answers.Value);
        }

        [HttpGet]
        public async Task<IActionResult> NotAnsweredCoursesQuestions(ClientSideFilterCourseQuestionViewModel model)
        {
            model.TeacherId = User.GetUserId();
            var answers = await _courseQuestionService.FilterNotAnsweredCourseQuestionsByMasterId(model);

            return View(answers.Value);
        }




        #endregion

        #region Question Answers

        [HttpGet]
        public async Task<IActionResult> QuestionAnswersList(ClientSideFilterCourseQuestionAnswerViewModel model)
        {
            model.UserId = User.GetUserId();
            var answers = await _courseQuestionAnswerService.GetCourseQuestionAnswersByUserIdClientUserPanelAsync(model);

            return View(answers.Value);
        }


        #endregion


        #endregion
    }
}
