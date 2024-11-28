using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Common;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseNotification;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.FavouriteCourse;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System.Net.Mime;

namespace Kidemy.Web.Controllers
{
    public class CoursesController : BaseController
    {
        #region Fields
        private readonly ICourseService _courseService;
        private readonly ICourseQuestionService _courseQuestionService;
        private readonly ICourseQuestionAnswerService _courseQuestionAnswerService;
        private readonly IStringLocalizer _localizer;
        private readonly ICourseCommentService _commentService;
        private readonly ILogger<CoursesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Ctor
        public CoursesController(ICourseService courseService,
            IStringLocalizer localizer,
            ICourseQuestionService courseQuestionService,
            ICourseCommentService commentService,
            ICourseQuestionAnswerService courseQuestionAnswerService,
            ILogger<CoursesController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _courseService = courseService;
            _localizer = localizer;
            _courseQuestionService = courseQuestionService;
            _courseQuestionAnswerService = courseQuestionAnswerService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _commentService = commentService;
        }
        #endregion

        #region Actions

        [HttpGet("course/{shortLink}")]
        public async Task<IActionResult> ShortLink(string shortLink)
        {
            if (string.IsNullOrWhiteSpace(shortLink)) return NotFound();

            int courseId;

            try
            {
                courseId = Convert.ToInt32(shortLink.Substring(2));
            }
            catch
            {
                return NotFound();
            }
            if (courseId < 1) return NotFound();

            var courseSlug = await _courseService.GetCourseSlugById(courseId);

            return RedirectToAction(nameof(CourseDetails), new { slug = courseSlug.Value });
        }

        [HttpGet("categories-list")]
        public async Task<IActionResult> CategoryList()
        {
            var result = await _courseService.GetCategoriesListClientSide();

            if (result.IsFailure)
            {
                List<ClientSideCategoryViewModel> model = new List<ClientSideCategoryViewModel>();
                return View(model);
            }
            return View(result.Value);
        }

        [HttpPost("/add-free-course-to-user-courses")]
        public async Task<ResponseResult> AddFreeCourseToUserCourses(int courseId)
        {
            if (courseId <= 0)
            {
                return ResponseResult.Failure(_localizer[ErrorMessages.FailedOperationError].ToString());
            }

            var result = await _courseService.AddFreeCourseToUserCourses(courseId, User.GetUserId());

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }

            return ResponseResult.Success(_localizer[result.Message].ToString());
        }

        [HttpGet("courses-list")]
        public async Task<IActionResult> CoursesList(ClientSideFilterCoursesViewModel filter)
        {
            filter.TakeEntity = 9;
            var result = await _courseService.FilterCoursesForClientSide(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            if (User.Identity.IsAuthenticated)
            {

                var favouriteCourses = await _courseService.GetAllUserFavouriteCoursesAsync(User.GetUserId());
                if (favouriteCourses.IsSuccess && favouriteCourses.Value.Any())
                    ViewBag.FavouriteCourses = favouriteCourses.Value;
            }

            return View(filter);
        }

        [HttpGet("course/video/{courseId}/{videoId}")]
        public async Task<IActionResult> GetVideo(int courseId, string videoId)
        {
            var videoFileNameResult = await _courseService.GetVideoFileNameForUser(courseId, int.Parse(videoId.Replace(".mp4", "")), User.GetUserId());

            if (videoFileNameResult.IsFailure)
            {
                _logger.LogError("CourseController: GetVideo: GetVideoFileNameForUser failed by message: " + videoFileNameResult.Message);

                return NotFound();
            }

            var filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(courseId) +
                           videoFileNameResult.Value.VideoFileName;

            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var fileInfo = provider.GetFileInfo(filePath);

            return File(fileInfo.CreateReadStream(), "video/mp4");
        }

        [HttpGet("course/download-video/{courseId}/{videoId}")]
        public async Task<IActionResult> GetVideoForDownload(int courseId, int videoId)
        {
            var videoFileNameResult = await _courseService.GetVideoFileNameForUser(courseId, videoId, User.GetUserId());

            if (videoFileNameResult.IsFailure)
            {
                _logger.LogError("CourseController: GetVideo: GetVideoFileNameForUser failed by message: " + videoFileNameResult.Message);

                return NotFound();
            }

            var videoFileName = videoFileNameResult.Value;

            var filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(courseId) +
                           videoFileName.VideoFileName;

            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var fileInfo = provider.GetFileInfo(filePath);

            var downloadFileName = $"{videoFileName.Priority.ToString("00")}_{videoFileName.SuffixFileName}{Path.GetExtension(videoFileName.VideoFileName)}";

            return File(fileInfo.CreateReadStream(), "video/mp4", downloadFileName);
        }

        [HttpGet("course/download-attachment/{courseId}/{videoId}")]
        public async Task<IActionResult> GetAttachmentForDownload(int courseId, int videoId)
        {
            var videoFileNameResult = await _courseService.GetVideoFileNameForUser(courseId, videoId, User.GetUserId());

            if (videoFileNameResult.IsFailure)
            {
                _logger.LogError("CourseController: GetVideo: GetVideoFileNameForUser failed by message: " + videoFileNameResult.Message);

                return NotFound();
            }

            var videoFileName = videoFileNameResult.Value;

            var filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(courseId) +
                           videoFileName.AttachmentFileName;

            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            var fileInfo = provider.GetFileInfo(filePath);

            var downloadFileName = $"{videoFileName.Priority.ToString("00")}_{videoFileName.SuffixFileName}{Path.GetExtension(videoFileName.AttachmentFileName)}";

            return File(fileInfo.CreateReadStream(), MediaTypeNames.Application.Octet, downloadFileName);
        }

        [HttpGet("course-details/{slug}")]
        public async Task<IActionResult> CourseDetails(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return RedirectToAction(nameof(CoursesList));
            }
            var result = await _courseService.GetCourseDetailClienSide(slug);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return RedirectToAction(nameof(CoursesList));
            }

            var course = result.Value;

            var courseViewResult = await _courseService.UpdateCourseView(result.Value.Id);

            var userCanPlayAllVideosOfCourseResult = await _courseService.UserCanPlayAllVideosOfCourse(course.Id, User.GetUserId());

            if (userCanPlayAllVideosOfCourseResult.IsFailure)
            {
                ViewData["UserCanPlayAllVideosOfCourse"] = false;
            }
            else
            {
                ViewData["UserCanPlayAllVideosOfCourse"] = userCanPlayAllVideosOfCourseResult.Value;
            }

            var favouriteCourses = await _courseService.GetAllUserFavouriteCoursesAsync(User.GetUserId());
            if (favouriteCourses.IsSuccess && favouriteCourses.Value.Any())
                ViewBag.FavouriteCourses = favouriteCourses.Value;

            return View(course);
        }

        [HttpGet("course-episodes/{slug}")]
        public async Task<IActionResult> LoadCourseEpisodes(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }
            var result = await _courseService.GetCourseVideosByCourseSlug(slug);
            if (result.IsFailure)
            {
                var model = new ClientSideCourseWithVideosViewModel();

                return PartialView("_CourseEpisodes", model);
            }

            var userCanPlayAllVideosOfCourse = await _courseService.UserCanPlayAllVideosOfCourse(result.Value.Id, User.GetUserId());

            ViewData["UserCanPlayAllVideosOfCourse"] = userCanPlayAllVideosOfCourse.IsSuccess ? userCanPlayAllVideosOfCourse.Value : false;

            var userHasCourseResult = await _courseService.UserHasCourseAsync(result.Value.Id, User.GetUserId());

            if (userHasCourseResult.IsSuccess)
            {
                ViewData["UserHasCourse"] = userHasCourseResult.Value;
            }

            return PartialView("_CourseEpisodes", result.Value);
        }

        #region LoadComments 

        [HttpGet("course-comments/")]
        public async Task<IActionResult> LoadComments(string slug, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            var allowCommentingResult = await _courseService.IsCommentsOpen(slug);
            if (allowCommentingResult.IsFailure)
            {
                return PartialView("_NotAllowedCommenting");
            }

            if (allowCommentingResult.Value == false)
                return PartialView("_NotAllowedCommenting");

            var result = await _courseService.GetCommentsForClientSide(slug, page);
            if (result.IsFailure)
            {
                return null;
            }

            if (!User?.Identity?.IsAuthenticated ?? true)
            {
                TempData["returnUrl"] = Url.Action("CourseDetails", new { slug = slug });
            }

            return PartialView("_Comments", result.Value);
        }

        [HttpGet("course-master/{slug}")]
        public async Task<IActionResult> LoadMaster(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }
            var result = await _courseService.GetMasterByCourseSlugAsync(slug);

            if (result.IsFailure)
            {
                return PartialView("_Master");
            }

            return PartialView("_Master", result.Value);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> CreateComments(ClientSideCreateCommentViewModel model)
        {
            if (!ModelState.IsValid) return ResponseResult.Failure(_localizer[ErrorMessages.FailedOperationError].ToString());
            var result = await _courseService.CreateComment(model);
            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
                return ResponseResult.Success();
            }
        }

        [Authorize]
        public IActionResult ReportComment(int commentId)
        {
            return PartialView("_ReportComment", new ClientSideReportCommentViewModel() { CommentId = commentId });
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> ReportComment(ClientSideReportCommentViewModel model)
        {
            if (model?.Message == null)
            {
                return new ResponseResult(false, _localizer[ErrorMessages.CommentReportMessageIsRequired], _localizer["Ok"].ToString());
            }

            if (!ModelState.IsValid)
            {
                return new ResponseResult(false, _localizer[ErrorMessages.FailedOperationError].ToString(), _localizer["Ok"].ToString());
            }

            var result = await _commentService.ReportCommentUserSide(model);
            if (result.IsFailure)
            {
                return new ResponseResult(false, _localizer[result.Message].ToString(), _localizer["Ok"].ToString());
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return ResponseResult.Success();
        }

        [Authorize]
        public async Task<IActionResult> ReplyComment(int replyCommentId)
        {
            return PartialView("_ReplyComment", new ClientSideCreateCommentViewModel() { ReplyForCommentId = replyCommentId });
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResponseResult> ReplyComment(ClientSideCreateCommentViewModel model)
        {
            if (model?.Message == null)
            {
                return new ResponseResult(false, _localizer[ErrorMessages.CommentReplyMessageRequiredError], _localizer["Ok"].ToString());
            }

            if (model.ReplyForCommentId is null)
            {
                return new ResponseResult(false, _localizer[ErrorMessages.FailedOperationError], _localizer["Ok"].ToString());
            }

            var result = await _commentService.CreateReplyForComment(model);
            if (result.IsFailure)
            {
                return new ResponseResult(false, _localizer[result.Message].ToString(), _localizer["Ok"].ToString());
            }

            TempData[SuccessMessage] = _localizer[result.Message].ToString();

            return ResponseResult.Success();
        }
        #endregion

        #region CourseQuestions 

        #region Create Question
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(ClientSideUpsertCourseQuestionViewModel model)
        {
            model.UserId = User.GetUserId();

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.PleaseFillAllRequiredFieldsError].ToString();
                return RedirectToAction(nameof(QuestionsList), new { CourseSlug = model.CourseSlug });
            } 
            var result = await _courseQuestionService.CreateAsync(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }
            return RedirectToAction(nameof(QuestionsList), new { CourseSlug = model.CourseSlug });
        }
        #endregion

        #region Answers List
      
        [HttpGet("Question-Answers-List/{questionId}")]
        public async Task<IActionResult> QuestionAnswersList(ClientSideFilterCourseQuestionAnswerViewModel model)
        {
            var result = await _courseQuestionAnswerService.GetCourseQuestionAnswersByQuestionIdForClientAsync(model);

            return View(result.Value);
        }
        #endregion

        #region Questions list
        public async Task<IActionResult> QuestionsList(ClientSideFilterCourseQuestionViewModel model)
        {
            var result = await _courseQuestionService.FilterCourseQuestionsBySlug(model);
            return View(result.Value);
        }
        #endregion

        #region CreateAnswers
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateQuestionAnswer(ClientSideUpsertCourseQuestionAnswerViewModel model)
        {
            model.AnsweredById = User.GetUserId();

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.PleaseFillAllRequiredFieldsError].ToString();
                return RedirectToAction(nameof(QuestionAnswersList), new { CourseSlug = model.QuestionId });
            }
            var result = await _courseQuestionAnswerService.CreateAsync(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }
            return RedirectToAction(nameof(QuestionAnswersList), new { QuestionId = model.QuestionId });
        }
        #endregion

        #region Delete Course Question
        public async Task<IActionResult> DeleteQuestion(int qeustionId,string slug)
        {
            if (qeustionId <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return RedirectToAction(nameof(QuestionsList), new { CourseSlug = slug });
            }

            var result = await _courseQuestionService.DeleteAsync(qeustionId);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }
            return RedirectToAction(nameof(QuestionsList), new { CourseSlug = slug });
        }

        #endregion

        #region DeleteCourse Question Answer

        public async Task<IActionResult> DeleteQuestionAnswer(int questionAnswerId,int questionId)
        {
            if (questionAnswerId <= 0)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
                return RedirectToAction(nameof(QuestionAnswersList), new { QuestionId = questionId });
            }

            var result = await _courseQuestionAnswerService.DeleteAsync(questionAnswerId);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[SuccessMessages.SuccessfullyDone].ToString();
            }
            return RedirectToAction(nameof(QuestionAnswersList), new { QuestionId = questionId });
        }

        #endregion

        #region User FeedBack

        public async Task<ResponseResult> UserFeedBack(int questionId, bool feedback)
        {

            if (!ModelState.IsValid) return ResponseResult.Failure(_localizer[ErrorMessages.FailedOperationError].ToString());
            var result = await _courseQuestionService.ChangeUserFeedBack(questionId, feedback);
            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }
            else
            {
                return ResponseResult.Success(_localizer[SuccessMessages.SuccessfullyDone].ToString());
            }
        }

        #endregion

        #endregion

        #region Today Discount

        [HttpGet("discounted-courses")]
        public async Task<IActionResult> TodayDiscount(ClientSideFilterCoursesViewModel filter)
        {
            filter.TakeEntity = 500;
            var result = await _courseService.GetDiscountedCoursesOfTheDay(filter);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
                return View(filter);
            }

            return View(filter);
        }

        #endregion

        #region favourite-Course
        [HttpGet]
        public async Task<ResponseResult> FavouriteCourse(int courseId)
        {
            if (!ModelState.IsValid) return ResponseResult.Failure(_localizer[ErrorMessages.FailedOperationError].ToString());

            if (!User.Identity.IsAuthenticated)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FirstLoginError].ToString();
                return ResponseResult.Failure(_localizer[ErrorMessages.FirstLoginError].ToString());
            }

            var favouriteModel = new ClientSideFavouriteCourseViewModel()
            {
                CourseId = courseId,
                UserId = User.GetUserId(),
            };

            var result = await _courseService.SetFavouriteCoursesAsync(favouriteModel);

            if (result.IsFailure)
            {
                return ResponseResult.Failure(_localizer[result.Message].ToString());
            }
            else
            {
                return ResponseResult.Success(_localizer[SuccessMessages.SuccessfullyDone].ToString(), new { courseId = courseId, isfavourite = result.Value });
            }
        }

        #endregion

        #region Course Notification
        [HttpGet]
        public async Task<IActionResult> CourseNotification(ClientSideAddCourseNotificationViewModel model, string courseSlug)
        {
            if (model == null || courseSlug == null)
            {
                TempData[ErrorMessage] = _localizer[ErrorMessages.FailedOperationError];
            }

            if (User.Identity.IsAuthenticated)
                model.UserId = User.GetUserId();

            var result = await _courseService.AddUserForCourseNotificationAsync(model);
            if (result.IsFailure)
            {
                TempData[ErrorMessage] = _localizer[result.Message].ToString();
            }
            else
            {
                TempData[SuccessMessage] = _localizer[result.Message].ToString();
            }

            return RedirectToAction(nameof(CourseDetails), new { slug = courseSlug });
        }

        #endregion

        #endregion
    }
}