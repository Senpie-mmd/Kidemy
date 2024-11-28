using AngleSharp.Common;
using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Convertors;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseNotification;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseTag;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Application.ViewModels.FavouriteCourse;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.Enums.Course;
using Kidemy.Domain.Enums.CourseNotification;
using Kidemy.Domain.Events.Course.Course;
using Kidemy.Domain.Events.Course.FavouriteCourse;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Web;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseService : ICourseService
    {
        #region Fields

        private readonly ICourseRepository _courseRepository;
        private readonly ICourseTagRepository _courseTagRepository;
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        private readonly ILocalizingService _localizing;
        private readonly ICourseCommentRepository _commentRepository;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDiscountService _discountService;
        private readonly ILogger<CourseService> _logger;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICourseViewRepository _courseViewRepository;
        private readonly IFavouriteCourseRepository _favouriteCourseRepository;
        private readonly IMasterService _masterService;
        private readonly IEncryptService _encryptService;
        private readonly IVIPMembersService _VIPMemebersService;
        private readonly ICourseNotificationRepository _courseNotificationRepository;

        #endregion

        #region Ctor
        public CourseService(ICourseRepository courseRepository,
            ICourseCategoryRepository courseCategoryRepository,
            ILocalizingService localizing,
            ICourseCommentRepository commentRepository,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IDiscountService discountService,
            ICourseTagRepository courseTagRepository,
            IMediatorHandler mediatorHandler,
            ICourseViewRepository courseViewRepository,
            IEncryptService encryptService,
            IVIPMembersService vIPMemebersService,
            ILogger<CourseService> logger,
            IMasterService masterService,
            IFavouriteCourseRepository favouriteCourseRepository,
            ICourseNotificationRepository courseNotificationRepository)
        {
            _courseRepository = courseRepository;
            _courseCategoryRepository = courseCategoryRepository;
            _localizing = localizing;
            _commentRepository = commentRepository;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _discountService = discountService;
            _courseTagRepository = courseTagRepository;
            _mediatorHandler = mediatorHandler;
            _logger = logger;
            _courseViewRepository = courseViewRepository;
            _encryptService = encryptService;
            _VIPMemebersService = vIPMemebersService;
            _masterService = masterService;
            _favouriteCourseRepository = favouriteCourseRepository;
            _courseNotificationRepository = courseNotificationRepository;
        }
        #endregion

        #region Methods

        public async Task<Result<List<ClientSideCategoryViewModel>>> GetCategoriesListClientSide()
        {
            var categories = await _courseCategoryRepository.GetCategoriesWithCountOfCourses(n => n.ParentCourseCategoryId == null);
            if (categories is null || !categories.Any()) return Result.Failure<List<ClientSideCategoryViewModel>>(ErrorMessages.FailedOperationError);

            var model = categories.Select(n => new ClientSideCategoryViewModel().MapFrom(n)).ToList();


            return model;
        }

        public async Task<Result<CourseVideoTitleViewModel>> GetVideoFileNameForUser(int courseId, int videoId, int userId)
        {
            if (courseId <= 0 || videoId <= 0)
            {
                return Result.Failure<CourseVideoTitleViewModel>(ErrorMessages.FailedOperationError);
            }

            var video = await _courseRepository.GetVideoAsync(videoId);


            if (video is null) return Result.Failure<CourseVideoTitleViewModel>(ErrorMessages.FailedOperationError);

            var suffixFileName = await _courseRepository.GetCourseSuffixName(courseId);

            var model = new CourseVideoTitleViewModel
            {
                SuffixFileName = suffixFileName,
                VideoFileName = video.VideoFileName,
                Priority = video.Priority,
                AttachmentFileName = video.AttachFileName
            };

            if (!video.IsPublished) return Result.Failure<CourseVideoTitleViewModel>(ErrorMessages.FailedOperationError);

            if (video.IsFree)
            {
                return model;
            }
            else
            {
                var userCanPlayAllVideosResult = await UserCanPlayAllVideosOfCourse(courseId, userId);

                if (userCanPlayAllVideosResult.IsFailure)
                {
                    return Result.Failure<CourseVideoTitleViewModel>(userCanPlayAllVideosResult.Message);
                }

                if (userCanPlayAllVideosResult.Value == false)
                {
                    return Result.Failure<CourseVideoTitleViewModel>(ErrorMessages.UserDoesNotHaveTheCourseError);
                }

                return model;
            }
        }

        public async Task<Result> AddFreeCourseToUserCourses(int courseId, int userId)
        {
            if (courseId <= 0 || userId <= 0)
            {
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            var expectedCourseToAddUser = await _courseRepository.FirstOrDefaultAsync(course => course.Id == courseId);

            if (expectedCourseToAddUser is null)
            {
                return Result.Failure(ErrorMessages.FailedOperationError);
            }

            if (expectedCourseToAddUser.Type != CourseType.Free) return Result.Failure(ErrorMessages.FailedOperationError);

            var userHasCourseResult = await UserHasCourseAsync(courseId, userId);

            if (userHasCourseResult.IsSuccess && userHasCourseResult.Value)
            {
                return Result.Failure(ErrorMessages.UserAlreadyBoughtCourseError);
            }

            expectedCourseToAddUser.Users ??= new List<CourseUserMapping>();
            expectedCourseToAddUser.Users.Add(new CourseUserMapping
            {
                UserId = userId,
                CourseId = expectedCourseToAddUser.Id
            });

            _courseRepository.Update(expectedCourseToAddUser);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<List<CourseCategoryWithSubCategoriesViewModel>>> GetCategoriesWithSubCategoriesAsync(List<int> courseCategoryIds)
        {
            if (courseCategoryIds == null || !courseCategoryIds.Any()) return new List<CourseCategoryWithSubCategoriesViewModel>();

            var categoriesWithSubCategories = await _courseCategoryRepository
                .GetCategoriesWithSubCategories(courseCategoryIds);

            if (categoriesWithSubCategories is null || !categoriesWithSubCategories.Any())
            {
                return new List<CourseCategoryWithSubCategoriesViewModel>();
            }

            var model = categoriesWithSubCategories.Select(n => new CourseCategoryWithSubCategoriesViewModel().MapFrom(n)).ToList();

            return model;
        }

        public async Task<Result<ClientSideFilterCoursesViewModel>> FilterCoursesForClientSide(ClientSideFilterCoursesViewModel filter)
        {
            if (filter is null) return Result.Failure<ClientSideFilterCoursesViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<Course>();

            conditions.Add(n => n.IsPublished);

            if (!string.IsNullOrWhiteSpace(filter.CourseTag))
            {
                var tagId = await _courseTagRepository.GetTagIdByTitle(filter.CourseTag);
                if (tagId > 1)
                {
                    conditions.Add(n => n.CourseTags.Any(tag => tag.TagId == tagId));
                }
            }

            if (filter.Level is not null)
            {
                conditions.Add(n => n.Level == filter.Level);
            }

            if (filter.MasterId is not null)
            {
                conditions.Add(c => c.MasterId == filter.MasterId);
            }

            if (filter.PriceType is not null)
            {
                switch (filter.PriceType)
                {
                    case CoursePriceTypeForView.All:
                        break;
                    case CoursePriceTypeForView.Free:
                        conditions.Add(n => n.Type == CourseType.Free);
                        break;
                    case CoursePriceTypeForView.Mercenary:
                        conditions.Add(n => n.Type == CourseType.Mercenary);
                        break;
                    case CoursePriceTypeForView.VIP:
                        conditions.Add(n => n.Type == CourseType.VIP);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(n => n.Title.Contains(filter.Title));
            }

            if (filter.CategoryIds is not null && filter.CategoryIds.Any())
            {
                if (!filter.CategoryIds.Contains(-1))
                {
                    conditions.Add(course => course.Categories
                    .Select(category => category.CategoryId)
                    .Union(course.Categories.Select(category => category.Category.ParentCourseCategoryId.Value))
                    .Union(course.Categories.Select(category => category.Category.ParentCategory.ParentCourseCategoryId.Value))
                    .Any(id => filter.CategoryIds.Contains(id)));

                }
            }
            else
            {
                filter.CategoryIds = new List<int>();

                // for filter all courses
                filter.CategoryIds.Add(-1);
            }

            if (filter.CourseRates is not null)
            {
                switch (filter.CourseRates)
                {
                    case FilterCoursesListEnum.MostViewed:
                        await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel, null, n => n.CourseViews.Count());
                        break;
                    case FilterCoursesListEnum.LastUpdated:
                        await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel, null, n => n.UpdatedDateOnUtc);
                        break;
                    case FilterCoursesListEnum.BestSelling:
                        await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel, null, n => n.Users.Count());
                        break;
                    case FilterCoursesListEnum.Newest:
                        await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel, null, n => n.CreatedDateOnUtc);
                        break;
                }
            }
            else
            {
                await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel);
            }


            await _localizing.TranslateModelAsync(filter);

            var applyDiscountResult = await _discountService.ApplyDiscount(filter.Entities);

            if (applyDiscountResult.IsFailure)
            {
                _logger.LogError("CourseService: FilterCoursesForClientSide: apply discount on model failed. message: " + applyDiscountResult.Message);
            }

            var categoriesResult = await _courseCategoryRepository.GetCategoriesWithCountOfCourses();

            filter.categories = new List<CategoriesForCourseListClientSideViewModel>();
            filter.categories.AddRange(categoriesResult.Select(n => new CategoriesForCourseListClientSideViewModel().MapFrom(n)));
            filter.AllCoursesCount = await _courseRepository.GetCountOfAllCourses();

            return filter;
        }

        public async Task<Result<ClientSideFilterCoursesViewModel>> GetDiscountedCoursesOfTheDay(ClientSideFilterCoursesViewModel filter)
        {
            var entities = new List<ClientSideCourseViewModel>();
            var currentPage = filter.Page;
            do
            {
                if (filter is null) return Result.Failure<ClientSideFilterCoursesViewModel>(ErrorMessages.FailedOperationError);

                var conditions = Filter.GenerateConditions<Course>();

                conditions.Add(n => n.IsPublished);

                conditions.Add(n => n.Type == CourseType.Mercenary);

                if (filter.Level is not null)
                {
                    conditions.Add(n => n.Level == filter.Level);
                }


                if (filter.PriceType is not null)
                {
                    switch (filter.PriceType)
                    {
                        case CoursePriceTypeForView.All:
                            break;
                        case CoursePriceTypeForView.Free:
                            conditions.Add(n => n.Price == 0);
                            break;
                        case CoursePriceTypeForView.Mercenary:
                            conditions.Add(n => n.Price != 0);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(filter.Title))
                {
                    conditions.Add(n => n.Title.Contains(filter.Title));
                }

                if (filter.CategoryIds is not null && filter.CategoryIds.Any())
                {
                    if (!filter.CategoryIds.Contains(-1))
                    {
                        conditions.Add(course => course.Categories
                        .Select(category => category.CategoryId)
                        .Union(course.Categories.Select(category => category.Category.ParentCourseCategoryId.Value))
                        .Union(course.Categories.Select(category => category.Category.ParentCategory.ParentCourseCategoryId.Value))
                        .Any(id => filter.CategoryIds.Contains(id)));

                    }
                }
                else
                {
                    filter.CategoryIds = new List<int>();

                    // for filter all courses
                    filter.CategoryIds.Add(-1);
                }

                await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModel);

                await _localizing.TranslateModelAsync(filter);


                var applyDiscountResult = await _discountService.ApplyDiscount(filter.Entities);

                if (applyDiscountResult.IsFailure)
                {
                    _logger.LogError("CourseService: FilterCoursesForClientSide: apply discount on model failed. message: " + applyDiscountResult.Message);
                }

                var categoriesResult = await _courseCategoryRepository.GetCategoriesWithCountOfCourses();

                filter.categories = new List<CategoriesForCourseListClientSideViewModel>();
                filter.categories.AddRange(categoriesResult.Select(n => new CategoriesForCourseListClientSideViewModel().MapFrom(n)));
                filter.AllCoursesCount = await _courseRepository.GetCountOfAllCourses();

                var newEntities = filter.Entities.Where(c => c.AppliedDiscount != null).ToList();
                entities.AddRange(newEntities);

                filter.Page++;
                if (filter.Page > filter.PageCount)
                {
                    break;
                }
            } while (entities.Count < filter.TakeEntity);

            filter.Entities = entities;
            filter.Page = currentPage;
            return filter;
        }

        public async Task<Result<int>> GetCourseCount()
        {
            return await _courseRepository.GetCountOfAllCourses();
        }

        public async Task<Result<List<CourseMasterModel>>> GetMastersOfCourses(List<int> courseIds)
        {
            if (courseIds?.Count == 0) return Result.Failure<List<CourseMasterModel>>(ErrorMessages.FailedOperationError);

            return await _courseRepository.GetMastersOfCourses(courseIds);
        }

        public async Task<Result<ClientSideCourseDetailViewModel>> GetCourseDetailClienSide(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Result.Failure<ClientSideCourseDetailViewModel>(ErrorMessages.FailedOperationError);
            var course = await _courseRepository.GetCourseWithTagsAndCategoryNameById(slug);

            if (course is null) return Result.Failure<ClientSideCourseDetailViewModel>(ErrorMessages.FailedOperationError);

            ClientSideCourseDetailViewModel model = new ClientSideCourseDetailViewModel().MapFrom(course);

            var result = await _discountService.ApplyDiscount(model);

            if (result.IsFailure)
            {
                _logger.LogError("CourseService: GetCourseDetailClienSide: apply discount on model failed. message: " + result.Message);
            }

            return model;
        }

        public async Task<Result<ClientSideCourseWithVideosViewModel>> GetCourseVideosByCourseSlug(string slug)
        {
            if (slug is null) return Result.Failure<ClientSideCourseWithVideosViewModel>(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetCourseWithVideosAsync(slug);

            if (course == null) return Result.Failure<ClientSideCourseWithVideosViewModel>(ErrorMessages.FailedOperationError);

            var model = new ClientSideCourseWithVideosViewModel().MapFrom(course);

            return model;
        }

        public async Task<Result<ClientSideFilterCommentsViewModel>> GetCommentsForClientSide(string slug, int page = 1)
        {
            var decodedSlug = HttpUtility.UrlDecode(slug);
            if (string.IsNullOrWhiteSpace(decodedSlug)) return Result.Failure<ClientSideFilterCommentsViewModel>(ErrorMessages.FailedOperationError);
            int courseId = await _courseRepository.GetCourseIdBySlug(decodedSlug);
            if (courseId < 1) return Result.Failure<ClientSideFilterCommentsViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseComment>();
            conditions.Add(n => n.CourseId == courseId);
            conditions.Add(n => n.IsConfirmed == true);
            conditions.Add(n => n.ReplyCommnetId == null);

            ClientSideFilterCommentsViewModel filter = new ClientSideFilterCommentsViewModel();
            filter.CourseSlug = decodedSlug;
            filter.Page = page;

            await _commentRepository.FilterAsync(filter, conditions, CourseMapper.MapCommentViewModel);

            var userIds = filter.Entities.Select(n => n.UserId).ToList();
            userIds.AddRange(filter.Entities.SelectMany(n => n.RepliedComments).Select(n => n.UserId).ToList());

            userIds = userIds.Distinct().ToList();

            var usersInfo = await _userService.GetUserNameAndUserProfileByUserId(userIds);
            filter.Entities = filter.Entities
                .Select(Comments => Comments
                .MapFrom(usersInfo.Value.FirstOrDefault(n => n.Id == Comments.UserId)))
                .ToList();

            foreach (var comment in filter?.Entities ?? new List<ClientSideCommentViewModel>())
            {
                foreach (var reply in comment.RepliedComments ?? new List<ClientSideCommentViewModel>())
                {
                    reply.UserName = usersInfo.Value.Where(n => n.Id == reply.UserId).Select(n => n.UserName).FirstOrDefault();
                    reply.UserProfile = usersInfo.Value.Where(n => n.Id == reply.UserId).Select(n => n.UserAvatar).FirstOrDefault();
                }
            }


            var commentsScores = await _commentRepository.GetAvrageCommentsScore(courseId);

            filter.AvrageCourseScore = commentsScores.GetAvrage();

            return filter;
        }

        public async Task<Result<string>> CreateComment(ClientSideCreateCommentViewModel model)
        {
            if (model is null) return Result.Failure<string>(ErrorMessages.FailedOperationError);
            if (model.CourseSlug == null || !await _courseRepository.AnyAsync(n => n.Slug == model.CourseSlug)) return Result.Failure<string>(ErrorMessages.FailedOperationError);
            var courseId = await _courseRepository.GetCourseIdBySlug(model.CourseSlug);

            CourseComment comment = new CourseComment()
            {
                CommentedById = _httpContextAccessor.HttpContext.User.GetUserId(),
                IsConfirmed = false,
                Message = model.Message,
                Score = model.Score,
                CourseId = courseId
            };

            await _commentRepository.InsertAsync(comment);
            await _commentRepository.SaveAsync();

            return Result.Success(model.CourseSlug, SuccessMessages.YourCommentIsAwaitingApproval);
        }

        public async Task<Result<List<CourseTitleModel>>> GetCourseTitles(List<int> ids)
        {
            if (ids?.Count == 0) return Result.Failure<List<CourseTitleModel>>(ErrorMessages.ProcessFailedError);

            var model = await _courseRepository.GetCourseTitles(ids);

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result<List<CourseCategoryTitleModel>>> GetCourseCategoryTitles(List<int> ids)
        {
            if (ids?.Count == 0) return Result.Failure<List<CourseCategoryTitleModel>>(ErrorMessages.ProcessFailedError);

            var model = await _courseCategoryRepository.GetCourseCategoryTitles(ids);

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result<List<CourseFullDetailsViewModel>>> GetCoursesFullDetailsByIdsAsync(List<int> ids)
        {
            if (ids?.Count == 0) return new List<CourseFullDetailsViewModel>();

            var course = await _courseRepository.GetCoursesWithCategories(ids);

            if (course is null) return Result.Failure<List<CourseFullDetailsViewModel>>(ErrorMessages.FailedOperationError);

            var model = course.Select(n => new CourseFullDetailsViewModel().MapFrom(n)).ToList();

            await _localizing.TranslateModelAsync(model);
            await _localizing.TranslateModelAsync(model.Select(c => c.Categories).ToList());

            return model;
        }

        public async Task<Result<List<CourseShortDetailsViewModel>>> GetCoursesShortDetailsByIdsAsync(List<int> ids)
        {
            if (ids?.Count == 0) return new List<CourseShortDetailsViewModel>();

            var course = await _courseRepository.GetCoursesWithCategories(ids);

            if (course is null) return Result.Failure<List<CourseShortDetailsViewModel>>(ErrorMessages.FailedOperationError);

            var model = course.Select(n => new CourseShortDetailsViewModel().MapFrom(n)).ToList();

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result<List<ClientSideCourseTagViewModel>>> GetCourseTagsList()
        {
            var courseTags = await _courseTagRepository.GetAllAsync();
            if (courseTags is null || !courseTags.Any()) return Result.Failure<List<ClientSideCourseTagViewModel>>(ErrorMessages.FailedOperationError);

            var model = courseTags.Select(n => new ClientSideCourseTagViewModel().MapFrom(n)).ToList();


            return model;
        }

        public async Task<Result<ClientSideCourseShortDetailsViewModel>> GetCourseForClientByIdAsync(int id)
        {
            if (id <= 0) return Result.Failure<ClientSideCourseShortDetailsViewModel>(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetByIdAsync(id);

            if (course is null) return Result.Failure<ClientSideCourseShortDetailsViewModel>(ErrorMessages.FailedOperationError);

            var model = new ClientSideCourseShortDetailsViewModel().MapFrom(course);

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result> UpdateCourseView(int courseId)
        {
            if (courseId < 1) return Result.Failure(ErrorMessages.FailedOperationError);
            if (!await _courseRepository.AnyAsync(n => n.Id == courseId)) return Result.Failure(ErrorMessages.FailedOperationError);
            var userIp = _httpContextAccessor.HttpContext.GetUserIP();

            if (await _courseViewRepository.AnyAsync(n => n.UserIp == userIp && n.CourseId == courseId)) return Result.Failure(ErrorMessages.FailedOperationError);

            CourseView courseView = new CourseView()
            {
                CourseId = courseId,
                UserIp = userIp
            };

            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
            {
                courseView.UserId = _httpContextAccessor.HttpContext.User.GetUserId();
            }

            await _courseViewRepository.InsertAsync(courseView);
            await _courseViewRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<bool>> IsCommentsOpen(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            if (await _courseRepository.AnyAsync(n => n.Slug == slug && n.AllowComenting == false))
                return false;

            return true;
        }

        public async Task<Result<List<ClientSideCourseShortDetailsViewModel>>> GetCoursesForClientByIdsAsync(List<int> ids)
        {
            if (ids?.Count == 0) return new List<ClientSideCourseShortDetailsViewModel>();

            var course = await _courseRepository.GetCoursesWithCategories(ids);

            if (course is null) return Result.Failure<List<ClientSideCourseShortDetailsViewModel>>(ErrorMessages.FailedOperationError);

            var model = course.Select(n => new ClientSideCourseShortDetailsViewModel().MapFrom(n)).ToList();

            await _localizing.TranslateModelAsync(model);

            return model;
        }

        public async Task<Result<List<AdminSideCourseViewModel>>> GetCoursesForAdminByIdsAsync(List<int> ids)
        {
            if (ids?.Count == 0) return new List<AdminSideCourseViewModel>();

            var course = await _courseRepository.GetCoursesWithCategories(ids);

            if (course is null) return Result.Failure<List<AdminSideCourseViewModel>>(ErrorMessages.FailedOperationError);

            var model = course.Select(n => new AdminSideCourseViewModel().MapFrom(n)).ToList();

            return model;
        }

        public async Task<Result<bool>> UserHasCourseAsync(int courseId, int userId)
        {
            if (courseId <= 0 || userId <= 0) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            return await _courseRepository.AnyAsync(c => c.Id == courseId && c.Users.Any(u => u.UserId == userId));
        }

        public async Task<Result<bool>> UserCanPlayAllVideosOfCourse(int courseId, int userId)
        {
            if (courseId <= 0) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            var userIsAdminResult = await _userService.UserIsAdminAsync(userId);

            if (userIsAdminResult.IsSuccess && userIsAdminResult.Value == true)
            {
                return true;
            }

            var currentUserIsMasterOfCourse = await _courseRepository
                .AnyAsync(course => course.Id == courseId && course.MasterId == userId);

            if (currentUserIsMasterOfCourse)
            {
                return true;
            }

            var userHasCourseResult = await UserHasCourseAsync(courseId, userId);
            var userHasCourse = userHasCourseResult.IsSuccess ? userHasCourseResult.Value : false;

            var courseType = await _courseRepository.GetCourseType(courseId);

            var userIsVIPMemberResult = await _VIPMemebersService.IsUserVIPMemberAsync(userId);
            var userIsVIPMember = userHasCourseResult.IsSuccess ? userIsVIPMemberResult.Value : false;

            if (courseType == CourseType.VIP && !userIsVIPMember)
            {
                return false;
            }

            if (courseType == CourseType.Mercenary && !userHasCourse)
            {
                return false;
            }

            return true;
        }

        public async Task<Result> AddCoursesForUser(int userId, List<int> courseIds)
        {
            if (userId <= 0 || courseIds?.Count == 0) return Result.Failure(ErrorMessages.FailedOperationError);

            var userCourses = await _courseRepository
                .GetAllAsync(course => courseIds.Contains(course.Id) && course.Users.Any(user => user.UserId == userId));

            if (userCourses?.Any(course => courseIds.Contains(course.Id)) ?? false)
            {
                return Result.Failure(ErrorMessages.UserAlreadyBoughtCourseError);
            }

            var expectedCoursesToAddUser = await _courseRepository.GetAllAsync(course => courseIds.Contains(course.Id));

            foreach (var course in expectedCoursesToAddUser)
            {
                course.Users ??= new List<CourseUserMapping>();
                course.Users.Add(new CourseUserMapping
                {
                    UserId = userId,
                    CourseId = course.Id
                });
            }

            _courseRepository.UpdateRange(expectedCoursesToAddUser);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }
        public async Task<Result<List<ClientSideMastersOtherCoursesViewModel>>> GetOfferedCoursesForMaster(int masterId, int currentCourseId)
        {
            var courses = await _courseRepository.GetCoursesList(n => n.IsPublished == true && n.MasterId == masterId && n.Id != currentCourseId, 10);

            if (!courses?.Any() ?? true)
            {
                return Result.Failure<List<ClientSideMastersOtherCoursesViewModel>>(ErrorMessages.NotExist);
            }

            if (courses?.Count() < 0)
                return Result.Failure<List<ClientSideMastersOtherCoursesViewModel>>(ErrorMessages.FailedOperationError);

            var userIds = courses.Select(n => n.MasterId).ToList();
            var userInfos = await _userService.GetUsersProfileDetails(userIds);

            var model = courses.Select(c => new ClientSideMastersOtherCoursesViewModel().MapFrom(c, userInfos.Where(n => n.UserId == c.MasterId).FirstOrDefault())).ToList();

            var result = await _discountService.ApplyDiscount(model);

            if (result.IsFailure)
            {
                _logger.LogError("CourseService: GetPopularCourse: apply discount on model failed. message: " + result.Message);
            }

            return model;
        }

        public async Task<Result<List<ClientSideHomePageOfferedCoursesViewModel>>> GetSuggetionCourse(int count)
        {
            string[] parameters = { "Comments", "Users", "Categories", "Videos" };

            var courses = await _courseRepository.GetAllAsync(c => c.IsOffer && c.IsPublished, null, null, parameters);

            if (courses is null || !courses.Any())
            {
                return Result.Failure<List<ClientSideHomePageOfferedCoursesViewModel>>(ErrorMessages.NotExist);
            }

            var model = courses
                .OrderByDescending(c => c.UpdatedDateOnUtc)
                .Take(count)
                .Select(c => new ClientSideHomePageOfferedCoursesViewModel().MapFrom(c))
                .ToList();

            var masterIds = model.Select(n => n.MasterId).ToList();
            var userInfos = await _userService.GetUserNameAndUserProfileByUserId(masterIds);

            var categoryIds = model.Select(n => n.CategoryId).ToList();
            var categoryTitles = await _courseCategoryRepository.GetCourseCategoryTitles(categoryIds);

            foreach (var item in model ?? new List<ClientSideHomePageOfferedCoursesViewModel>())
            {
                item.MasterFullName = userInfos.Value.FirstOrDefault(n => n.Id == item.MasterId)?.UserName ?? "-";
                item.MasterImageFileName = userInfos.Value.FirstOrDefault(n => n.Id == item.MasterId)?.UserAvatar ?? "-";
                item.CategoryName = categoryTitles.FirstOrDefault(n => n.CategoryId == item.CategoryId)?.Title ?? "-";
            }

            var result = await _discountService.ApplyDiscount(model);

            if (result.IsFailure)
            {
                _logger.LogError("CourseService: GetPopularCourse: apply discount on model failed. message: " + result.Message);
            }

            return model;
        }

        public async Task<Result<int>> GetRecordingCourseCountAsync()
        {
            return await _courseRepository.GetRecordingCourseCountAsync();
        }

        public async Task<Result<ClientSideFilterCourseForUserPanelViewModel>> FilterCoursesOfUser(ClientSideFilterCourseForUserPanelViewModel filter)
        {
            if (filter is null) return Result.Failure<ClientSideFilterCourseForUserPanelViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<Course>();

            conditions.Add(n => n.IsPublished);

            if (filter.UserId.HasValue && filter.UserId > 0)
            {
                conditions.Add(c => c.Users.Any(u => u.UserId == filter.UserId));
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(c => c.Title.Contains(filter.Title));
            }

            if (filter.Type.HasValue)
            {
                conditions.Add(c => c.Type == filter.Type);
            }

            await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapUserPanelCourseViewModel);

            await _localizing.TranslateModelAsync(filter);

            return filter;
        }

        public async Task<Result<ClientSideMasterDetailsViewModel>> GetMasterByCourseSlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return Result.Failure<ClientSideMasterDetailsViewModel>(ErrorMessages.ProcessFailedError);

            var course = await _courseRepository.FirstOrDefaultAsync(c => c.Slug == slug);

            var model = new ClientSideMasterDetailsViewModel();

            if (course != null)
            {
                var master = await _masterService.GetMasterDetailsByIdAsync(course.MasterId);

                if (master.IsSuccess)
                {
                    model = master.Value;
                }
            }

            return model;

        }

        public async Task<Result<List<ClientSideLastCoursesViewModel>>> GetLastCourses()
        {
            var courses = await _courseRepository.GetCoursesList(null, 2, null, n => n.CreatedDateOnUtc);

            if (!courses?.Any() ?? true)
            {
                return Result.Failure<List<ClientSideLastCoursesViewModel>>(ErrorMessages.NotExist);
            }

            if (courses?.Count() < 1)
                return Result.Failure<List<ClientSideLastCoursesViewModel>>(ErrorMessages.FailedOperationError);

            var model = courses.Select(c => new ClientSideLastCoursesViewModel().MapFrom(c)).ToList();

            var result = await _discountService.ApplyDiscount(model);

            if (result.IsFailure)
            {
                _logger.LogError("CourseService: GetPopularCourse: apply discount on model failed. message: " + result.Message);
            }

            return model;
        }

        public async Task<Result<List<ClientSideCourseViewModel>>> GetLastCoursesForHomePage()
        {
            var courses = await _courseRepository.GetLastCourses(12);

            if (!courses?.Any() ?? true)
            {
                return Result.Failure<List<ClientSideCourseViewModel>>(ErrorMessages.NotExist);
            }

            if (courses?.Count() < 1)
                return Result.Failure<List<ClientSideCourseViewModel>>(ErrorMessages.FailedOperationError);

            var model = courses.Select(cource => new ClientSideCourseViewModel()
            {
                Id = cource.Id,
                Title = cource.Title,
                Slug = cource.Slug,
                Description = cource.ShortDescription,
                Level = cource.Level,
                ImageFileName = cource.ImageFileName,
                VideosCount = cource.VideosCount,
                AvrageScore = cource.ScoresList.GetAvrage(),
                CourseVideosLength = cource.ListOfTimeSpans
            }).ToList();

            return model;
        }

        public async Task<Result<bool>> CheckIfCourseVideoCanBeFree(int courseId)
        {
            if (courseId < 1) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            if (await _courseRepository.AnyAsync(n => n.Id == courseId && n.Type == CourseType.Free))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Result<CourseType>> GetCourseType(int courseId)
        {
            if (courseId < 1) return Result.Failure<CourseType>(ErrorMessages.FailedOperationError);

            return await _courseRepository.GetCourseType(courseId);
        }

        public async Task<Result<List<ClientSideMainPagePopularCoursesViewModel>>> GetPopularCourses()
        {
            var Categories = await _courseCategoryRepository.GetPopularCategoriesWithCourses();

            List<ClientSideMainPagePopularCoursesViewModel> model = new List<ClientSideMainPagePopularCoursesViewModel>();

            foreach (var item in Categories ?? new List<PopularCourseCategoriesModel>())
            {
                ClientSideMainPagePopularCoursesViewModel category = new ClientSideMainPagePopularCoursesViewModel()
                {
                    CategoryTitle = item.Title,
                    Id = item.Id,
                    PopularCourses = new List<ClientSideCourseViewModel>()
                };

                foreach (var item2 in item.PopularCourses ?? new List<CoursesForPopularCategoriesModel>())
                {
                    category.PopularCourses.Add(new ClientSideCourseViewModel
                    {
                        Id = item2.Id,
                        Title = item2.Title,
                        Slug = item2.Slug,
                        Description = item2.ShortDescription,
                        Level = item2.Level,
                        ImageFileName = item2.ImageFileName,
                        VideosCount = item2.VideosCount,
                        AvrageScore = item2.ScoresList.GetAvrage(),
                        CourseVideosLength = item2.ListOfTimeSpans
                    });
                }

                model.Add(category);
            }

            return model;
        }

        #region AdminSide

        public async Task<Result<AdminSideFilterCourseViewModel>> FilterCoursesAdminSide(AdminSideFilterCourseViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCourseViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<Course>();

            if (filter.Level is not null)
            {
                conditions.Add(n => n.Level == filter.Level);
            }

            if (filter.CourseStatus is not null)
            {
                conditions.Add(n => n.Status == filter.CourseStatus);
            }


            if (filter.PriceType is not null)
            {
                switch (filter.PriceType)
                {
                    case CourseType.VIP:
                        conditions.Add(n => n.Type == CourseType.VIP);
                        break;
                    case CourseType.Free:
                        conditions.Add(n => n.Type == CourseType.Free);
                        break;
                    case CourseType.Mercenary:
                        conditions.Add(n => n.Type == CourseType.Mercenary);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(n => n.Title.Contains(filter.Title));
            }


            await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCourseViewModelAdminSide);

            await _localizing.TranslateModelAsync(filter.Entities);

            //var result2 = await _courseCategoryRepository.GetCategoriesWithCountOfCourses();

            return filter;
        }

        public async Task<Result<bool>> IsThereAnyCourseWithThisCategoryId(int id)
        {
            if (await _courseRepository.AnyAsync(n => n.Categories.Any(n => n.CategoryId == id)))
            {
                return true;
            }

            return Result.Failure<bool>(ErrorMessages.FailedOperationError);
        }

        public async Task<int> GetCourseIdBySlug(string slug)
        {
            return await _courseRepository.GetCourseIdBySlug(slug);
        }

        public async Task<Result> CreateNewCourse(AdminSideCreateCourseViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.CourseImageFileName.Length > 7_000_000) return Result.Failure(ErrorMessages.ImageSizeError);
            if (!model.CourseImageFileName.ContentType.Contains("image/")) return Result.Failure(ErrorMessages.FileTypeError);

            if (await _courseRepository.AnyAsync(n => n.Slug == model.Slug)) return Result.Failure(ErrorMessages.DuplicateSlug);

            if (model.MasterCommissionPercentage > 100 || model.MasterCommissionPercentage < 0) return Result.Failure(ErrorMessages.MasterCommissionPercentageShouldBeBetweenZeroAndOneHundredError);

            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.CourseImageFileName.FileName);
            model.CourseImageFileName.AddImageToServer(imageName, SiteTools.CourseImagesPath, null, null, null, null, true);

            Course course = new Course()
            {
                Title = model.Title,
                ShortDescription = model.ShortDescription,
                Description = model.Description,
                Slug = model.Slug.Replace(" ", "-"),
                Level = model.Level,
                Status = model.Status,
                IsOffer = model.IsOffer,
                MasterId = model.MasterId,
                HasCertificate = model.HasCertificate,
                MasterCommissionPercentage = model.MasterCommissionPercentage ?? 0,
                FileSuffix = model.FileSuffix,
                AllowComenting = model.AllowComenting,
                Type = model.Type,
                IsPublished = model.IsPublished,
                ImageName = imageName,
                DemoVideoFileName = model.DemoVideoFileName
            };

            if (model.Type == CourseType.Mercenary)
            {
                if (model.Price is null || model.Price <= 0) return Result.Failure(ErrorMessages.CoursePriceError);

                course.Price = model.Price;
            }
            else
            {
                course.Price = null;
            }

            course.Categories = new List<CourseCategoryMapping>();

            foreach (var item in model.CourseCategoryIds)
            {
                course.Categories.Add(new CourseCategoryMapping
                {
                    CategoryId = item
                });
            }

            course.CourseTags = new List<CourseTagMapping>();

            var addedTags = model.CourseTags.Split(",").ToList();
            var insertedTagsInDB = await _courseTagRepository.GetTagsThatInsertedInDB(addedTags);
            var notInsertedInDB = addedTags.Except(insertedTagsInDB.Select(n => n.Title).ToList()).ToList();

            if (insertedTagsInDB?.Any() ?? false)
            {
                foreach (var DbTags in insertedTagsInDB)
                {
                    course.CourseTags.Add(new CourseTagMapping
                    {
                        TagId = DbTags.Id
                    });
                }
            }

            if (notInsertedInDB?.Any() ?? false)
            {
                foreach (var notInDbTags in notInsertedInDB)
                {
                    course.CourseTags.Add(new CourseTagMapping
                    {
                        Tag = new CourseTag { Title = notInDbTags }
                    });
                }
            }

            await _courseRepository.InsertAsync(course);
            await _courseRepository.SaveAsync();

            CourseCreatedEvent createEvent = new CourseCreatedEvent(
                course.Id, course.Title, course.ShortDescription, course.Description, course.Slug, course.Level, course.Status, course.IsOffer,
                course.MasterId, course.HasCertificate, course.AllowComenting, course.Type, course.IsPublished, course.ImageName,
                course.DemoVideoFileName, model.CourseTags.Split(",").ToList(), model.CourseCategoryIds);

            await _mediatorHandler.PublishEvent(createEvent);

            await _localizing.SaveLocalizations(course, model);

            return Result.Success();
        }

        public async Task<Result<AdminSideUpdateCourseViewModel>> GetCourseForEdit(int id)
        {
            if (id < 1) return Result.Failure<AdminSideUpdateCourseViewModel>(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetCourseByIdWithTagsAndCategories(id);
            if (course is null) return Result.Failure<AdminSideUpdateCourseViewModel>(ErrorMessages.FailedOperationError);

            var masterFullName = await _userService.GetUserFullName(course.MasterId);

            var model = new AdminSideUpdateCourseViewModel().MapFrom(course, masterFullName.Value);

            await _localizing.FillModelToEditByAdminAsync(course, model);

            return model;
        }

        public async Task<Result> UpdateCourse(AdminSideUpdateCourseViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetCourseByIdWithTagsAndCategories(model.Id);
            if (course is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (await _courseRepository.AnyAsync(n => n.Id != model.Id && n.Slug == model.Slug)) return Result.Failure(ErrorMessages.DuplicateSlug);

            if (model.MasterCommissionPercentage > 100 || model.MasterCommissionPercentage < 0) return Result.Failure(ErrorMessages.MasterCommissionPercentageShouldBeBetweenZeroAndOneHundredError);

            var copy = course.DeepCopy<Course>();

            if (model.CourseImageFileName != null)
            {
                if (model.CourseImageFileName.Length > 10_000_000) return Result.Failure(ErrorMessages.ImageSizeError);
                if (!model.CourseImageFileName.ContentType.Contains("image/")) return Result.Failure(ErrorMessages.FileTypeError);

                course.ImageName.DeleteImage(SiteTools.CourseImagesPath);

                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.CourseImageFileName.FileName);
                model.CourseImageFileName.AddImageToServer(imageName, SiteTools.CourseImagesPath, null, null, null, null, true);

                course.ImageName = imageName;
            }

            course.Title = model.Title;
            course.ShortDescription = model.ShortDescription;
            course.Description = model.Description;
            course.Slug = model.Slug.Replace(" ", "-");
            course.Level = model.Level;
            course.Status = model.Status;
            course.IsOffer = model.IsOffer;
            course.MasterId = model.MasterId;
            course.MasterCommissionPercentage = model.MasterCommissionPercentage ?? 0;
            course.FileSuffix = model.FileSuffix;
            course.HasCertificate = model.HasCertificate;
            course.AllowComenting = model.AllowComenting;
            course.Type = model.Type;
            course.IsPublished = model.IsPublished;
            course.DemoVideoFileName = model.DemoVideoFileName;
            course.UpdatedDateOnUtc = DateTime.UtcNow;

            if (model.Type == CourseType.Mercenary)
            {
                if (model.Price is null || model.Price <= 0) return Result.Failure(ErrorMessages.CoursePriceError);

                course.Price = model.Price;
            }
            else
            {
                course.Price = null;
            }

            var RemovedCategories = course.Categories.Where(n => !model.CourseCategoryIds.Contains(n.CategoryId)).ToList();
            if (RemovedCategories?.Any() ?? false)
            {
                foreach (var item in RemovedCategories)
                {
                    course.Categories.Remove(item);
                }
            }

            var existsCategoryIds = course.Categories.Where(n => model.CourseCategoryIds.Contains(n.CategoryId)).Select(n => n.CategoryId).ToList();
            var addedCategoryIds = model.CourseCategoryIds.Except(existsCategoryIds).ToList();

            if (addedCategoryIds is not null && addedCategoryIds.Any())
            {
                foreach (var categoryId in addedCategoryIds)
                {
                    course.Categories.Add(new CourseCategoryMapping
                    {
                        CategoryId = categoryId
                    });
                }
            }

            List<int> removedTagIds = course.CourseTags.Where(n => !model.CourseTags.Contains(n.Tag.Title)).Select(n => n.TagId).ToList();
            if (removedTagIds?.Any() ?? false)
            {
                foreach (var tagId in removedTagIds)
                {
                    course.CourseTags.Remove(course.CourseTags.First(n => n.TagId == tagId));
                }
            }

            var existTags = course.CourseTags.Where(n => model.CourseTags.Contains(n.Tag.Title) && n.CourseId == course.Id).Select(n => n.Tag.Title).ToList();
            var addedTags = model.CourseTags
                .Split(",").ToList()
                .Except(existTags).ToList();

            if (addedTags?.Any() ?? false)
            {
                var tagsAlreadyAdded = await _courseTagRepository.GetTagsThatInsertedInDB(addedTags);
                if (tagsAlreadyAdded is not null && tagsAlreadyAdded.Any())
                {
                    foreach (var alreadyAddedTag in tagsAlreadyAdded)
                    {
                        course.CourseTags.Add(new CourseTagMapping
                        {
                            TagId = alreadyAddedTag.Id,
                            CourseId = course.Id
                        });
                    }
                }
                else
                {
                    var tagsNotAdded = addedTags.Except(tagsAlreadyAdded.Select(n => n.Title).ToList()).ToList();
                    foreach (var item in tagsNotAdded)
                    {
                        course.CourseTags.Add(new CourseTagMapping
                        {
                            Tag = new CourseTag { Title = item }
                        });
                    }
                }
            }

            _courseRepository.Update(course);
            await _courseRepository.SaveAsync();

            CourseUpdatedEvent updateEvent = new CourseUpdatedEvent(
                copy.Id,
                copy.Title, copy.ShortDescription, copy.Description, copy.Slug, copy.Level, copy.Status,
                copy.IsOffer, copy.MasterId, copy.HasCertificate, copy.AllowComenting, copy.Type, copy.IsPublished,
                copy.ImageName, copy.DemoVideoFileName, copy.CourseTags.Select(n => n.Tag.Title).ToList(), copy.Categories.Select(n => n.CategoryId).ToList(),

                course.Title, course.ShortDescription, course.Description, course.Slug, course.Level, course.Status, course.IsOffer, course.MasterId,
                course.HasCertificate, course.AllowComenting, course.Type, course.IsPublished, course.ImageName, course.DemoVideoFileName,
                model.CourseTags.Split(",").ToList(), model.CourseCategoryIds
                );

            await _mediatorHandler.PublishEvent(updateEvent);

            await _localizing.SaveLocalizations(course, model);

            return Result.Success();
        }

        public async Task<Result> SoftDeleteCourseById(int id)
        {
            if (id < 1) return Result.Failure(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetByIdAsync(id);
            if (course is null) return Result.Failure(ErrorMessages.FailedOperationError);

            course.IsDeleted = true;

            _courseRepository.Update(course);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }


        #endregion

        #region FavouriteCourse

        public async Task<Result<bool>> SetFavouriteCoursesAsync(ClientSideFavouriteCourseViewModel model)
        {
            if (model.UserId < 1 || model.CourseId < 1) return Result.Failure<bool>(ErrorMessages.FailedOperationError);

            var existRecord = await _favouriteCourseRepository.FirstOrDefaultAsync(x => x.CourseId == model.CourseId && x.UserId == model.UserId);

            if (existRecord != null)
            {
                if (!existRecord.IsDeleted)
                {
                    await _favouriteCourseRepository.SoftDelete(existRecord.Id);
                    await _favouriteCourseRepository.SaveAsync();
                    return false;
                }
                existRecord.IsDeleted = false;
                _favouriteCourseRepository.Update(existRecord);
                await _favouriteCourseRepository.SaveAsync();
                return true;
            }

            var favouriteCourse = new FavouriteCourse()
            {
                CourseId = model.CourseId,
                UserId = model.UserId,
            };

            await _favouriteCourseRepository.InsertAsync(favouriteCourse);
            await _favouriteCourseRepository.SaveAsync();


            FavouriteCourseCreatedEvent createEvent = new FavouriteCourseCreatedEvent(
                favouriteCourse.UserId, favouriteCourse.CourseId
                );

            await _mediatorHandler.PublishEvent(createEvent);

            return true;

        }

        public async Task<Result<ClientSideFilterFavouriteCourseViewModel>> FilterUserFavouriteCoursesAsync(ClientSideFilterFavouriteCourseViewModel model)
        {
            if (model.UserId <= 0) return Result.Failure<ClientSideFilterFavouriteCourseViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<FavouriteCourse>();
            conditions.Add(n => n.UserId == model.UserId && !n.IsDeleted);

            await _favouriteCourseRepository.FilterAsync(model, conditions, FavouriteCourseMapper.MapClientSideFavouriteCourseViewModel, orderByDesc: x => x.Id);

            var courseIds = model.Entities.Select(x => x.CourseId).ToList();
            var courseSlugs = await GetCourseSlugsByUserIds(courseIds);
            model.Entities = model.Entities
                .Select(x => x
                .MapFrom(courseSlugs.Value.First(n => n.Id == x.CourseId)))
                .ToList();

            return model;
        }

        public async Task<Result<List<ClientSideGetSlugsViewModel>>> GetCourseSlugsByUserIds(List<int> ids)
        {

            if (ids.Count() < 1) return Result.Failure<List<ClientSideGetSlugsViewModel>>(ErrorMessages.FailedOperationError);
            var courseSlugList = new List<ClientSideGetSlugsViewModel>();

            foreach (var id in ids)
            {
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null) return Result.Failure<List<ClientSideGetSlugsViewModel>>(ErrorMessages.FailedOperationError);

                var clientSideGetSlugs = new ClientSideGetSlugsViewModel()
                {
                    Slugs = course.Slug,
                    Title = course.Title,
                    Id = course.Id,
                };
                courseSlugList.Add(clientSideGetSlugs);
            }

            return courseSlugList;
        }

        #endregion

        #region UserPanel

        public async Task<Result<string>> GetCourseSlugById(int courseId)
        {
            if (courseId < 1) return Result.Failure<string>(ErrorMessages.FailedOperationError);

            var slug = await _courseRepository.GetCourseSlugById(courseId);
            if (slug is null) return Result.Failure<string>(ErrorMessages.FailedOperationError);

            return slug;
        }
        public async Task<Result<AdminSideCourseViewModel>> GetCourseById(int courseId)
        {
            if (courseId < 1) return Result.Failure<AdminSideCourseViewModel>(ErrorMessages.FailedOperationError);

            var course = await _courseRepository.GetByIdAsync(courseId);
            var courseModel = new AdminSideCourseViewModel();
            if (course is not null)
            {
                courseModel.Title = course.Title;
                courseModel.Slug = course.Slug;
            }

            return courseModel;
        }

        public async Task<Result<List<int>>> GetAllUserFavouriteCoursesAsync(int userId)
        {
            if (userId < 1) return Result.Failure<List<int>>(ErrorMessages.FailedOperationError);

            var favourits = await _favouriteCourseRepository.GetAllAsync(x => x.UserId == userId && !x.IsDeleted);

            if (favourits != null && favourits.Any())
            {
                return favourits.Select(x => x.CourseId).ToList();
            }

            return new List<int>();
        }

        public async Task<Result<FilterUserPanelCoursesViewModel>> UserPanelFilterCourses(FilterUserPanelCoursesViewModel filter)
        {
            if (filter is null) return Result.Failure<FilterUserPanelCoursesViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<Course>();

            conditions.Add(n => n.MasterId == _httpContextAccessor.HttpContext.User.GetUserId());

            await _courseRepository.FilterAsync(filter, conditions, CourseMapper.MapCoursesListForMaster);


            return filter;

        }

        public async Task<Result<List<int>>> GetAllUsersNotificationByCourseIdAsync(int courseId, CourseNotificationType type)
        {
            var allUser = await _courseNotificationRepository.GetAllAsync(x => x.CourseId == courseId && x.CourseNotificationType == type);

            if (allUser != null && allUser.Any())
                return allUser.Select(x => x.UserId).ToList();

            return new List<int>();

        }

        public async Task<Result> AddUserForCourseNotificationAsync(ClientSideAddCourseNotificationViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);
            if (model.UserId == null || model.UserId <= 0) return Result.Failure(ErrorMessages.FirstLoginError);

            var exist = await _courseNotificationRepository.FirstOrDefaultAsync(x => x.CourseId == model.CourseId && x.UserId == model.UserId && x.CourseNotificationType == model.Type);

            if (exist is not null) return Result.Failure(ErrorMessages.YouDoneThisBefore);

            var courseNotification = new CourseNotification()
            {
                CourseId = model.CourseId,
                UserId = model.UserId,
            };
            await _courseNotificationRepository.InsertAsync(courseNotification);
            await _courseNotificationRepository.SaveAsync();
            return Result.Success(SuccessMessages.SuccessfullyDone);
        }

        #endregion

        #endregion
    }
}