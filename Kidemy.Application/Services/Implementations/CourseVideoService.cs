using AngleSharp.Html.Dom;
using Barnamenevisan.Localizing.Generator;
using Barnamenevisan.Localizing.Services;
using Kidemy.Application.Mapper;
using Kidemy.Application.Mediator;
using Kidemy.Application.Services.Interfaces;
using Kidemy.Application.Tools;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseVideos;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Domain.Events.Course.CourseVideo;
using Kidemy.Domain.Interfaces.Course;
using Kidemy.Domain.Models.Course;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PARSGREEN.API.SMS.Profile;

namespace Kidemy.Application.Services.Implementations
{
    public class CourseVideoService : ICourseVideoService
    {
        #region Fields 

        private readonly ICourseVideoRepository _videoRepository;
        private readonly ILocalizingService _localizingService;
        private readonly ICourseVideoCategoryRepository _videoCategoryRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor

        public CourseVideoService(ICourseVideoRepository videoRepository,
            ILocalizingService localizingService,
            ICourseVideoCategoryRepository videoCategoryRepository,
            ICourseRepository courseRepository,
            IMediatorHandler mediatorHandler,
            IHttpContextAccessor httpContextAccessor)
        {
            _videoRepository = videoRepository;
            _localizingService = localizingService;
            _videoCategoryRepository = videoCategoryRepository;
            _courseRepository = courseRepository;
            _mediatorHandler = mediatorHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        public async Task<Result> PublishCourseVideoById(int id)
        {
            var courseVideo = await _videoRepository.GetByIdAsync(id);
            if (courseVideo is null)
                return Result.Failure(ErrorMessages.NullValue);

            courseVideo.IsPublished = true;

            _videoRepository.Update(courseVideo);
            await _videoRepository.SaveAsync();
            return Result.Success();
        }

        public async Task<Result<AdminSideFilterCourseVideoViewModel>> FilterCourseVideosCategory(AdminSideFilterCourseVideoViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseVideo>();

            conditions.Add(n => n.CourseId == filter.CourseId);

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                conditions.Add(n => n.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrWhiteSpace(filter.CourseVideoCategoryTitle))
            {
                conditions.Add(n => n.VideoCategory.Title.Contains(filter.CourseVideoCategoryTitle));
            }

            if (filter.IsFree is not null)
            {
                conditions.Add(n => n.IsFree == filter.IsFree);
            }

            if (filter.IsPublished is not null)
            {
                conditions.Add(n => n.IsPublished == filter.IsPublished);
            }

            await _videoRepository.FilterAsync(filter, conditions, CourseVideoMapper.MapCourseVideos);

            await _localizingService.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result<AdminSideFilterCourseVideoViewModel>> FilterCourseVideosForDashboard(AdminSideFilterCourseVideoViewModel filter)
        {
            if (filter is null) return Result.Failure<AdminSideFilterCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseVideo>();

            if (filter.IsPublished is not null)
            {
                conditions.Add(n => n.IsPublished == filter.IsPublished);
            }

            await _videoRepository.FilterAsync(filter, conditions, CourseVideoMapper.MapCourseVideos);

            await _localizingService.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result<List<AdminSideCourseVideoCategoryViewModel>>> GetCourseVideoCategoryAsOptions(int courseId)
        {
            if (courseId < 1) return Result.Failure<List<AdminSideCourseVideoCategoryViewModel>>(ErrorMessages.FailedOperationError);

            var videoCategories = await _videoCategoryRepository.GetAllAsync(n => n.CourseId == courseId);

            if (!videoCategories?.Any() ?? true) return Result.Failure<List<AdminSideCourseVideoCategoryViewModel>>(ErrorMessages.FailedOperationError);

            return videoCategories.Select(n => new AdminSideCourseVideoCategoryViewModel().MapFrom(n)).ToList();
        }

        public async Task<Result> CreateNewCourseVideo(AdminSideCreateCourseVideoViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!await _courseRepository.AnyAsync(n => n.Id == model.CourseId)) return Result.Failure(ErrorMessages.FailedOperationError);

            if (model.VideoLength.Length != 8) return Result.Failure(ErrorMessages.InvalidVideoLengthError);

            var courseVideoLength = new TimeSpan();

            try
            {
                var timesList = model.VideoLength.Split(":").ToList();
                courseVideoLength = new TimeSpan(Convert.ToInt32(timesList[0]), Convert.ToInt32(timesList[1]), Convert.ToInt32(timesList[2]));

                if (courseVideoLength > new TimeSpan(23, 59, 59) || courseVideoLength < new TimeSpan(00, 00, 01)) return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }
            catch
            {
                return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }

            if (await _videoRepository.AnyAsync(n => n.CourseId == model.CourseId && n.Priority == model.Priority)) return Result.Failure(ErrorMessages.DuplicateSessionNumberError);

            //if (model.VideoThumbnailImage.Length > 10_000_000) return Result.Failure(ErrorMessages.ImageSizeError);

            //if (!model.VideoThumbnailImage.ContentType.Contains("image/")) return Result.Failure(ErrorMessages.FileTypeError);

            //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.VideoThumbnailImage.FileName);
            string filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(model.CourseId);

            //model.VideoThumbnailImage.AddImageToServer(fileName, filePath, null, null);

            CourseVideo video = new CourseVideo()
            {
                CourseId = model.CourseId,
                IsFree = model.IsFree,
                IsPublished = model.IsPublished,
                Title = model.Title,
                //ThumbnailImageName = fileName,
                Priority = model.Priority,
                VideoFileName = model.VideoFileName,
                VideoLength = courseVideoLength,
                AttachFileName = model.AttachFileName
            };

            if (await _videoCategoryRepository.AnyAsync(n => n.Title == model.CourseVideoCategoryTitle))
            {
                video.VideoCategoryId = await _videoCategoryRepository.GetVideoCategoryIdByTitle(model.CourseVideoCategoryTitle);
            }
            else
            {
                video.VideoCategory = new CourseVideoCategory()
                {
                    Title = model.CourseVideoCategoryTitle,
                    CourseId = model.CourseId
                };
            }

            await _videoRepository.InsertAsync(video);
            await _videoRepository.SaveAsync();

            await _localizingService.SaveLocalizations(video, model);

            CourseVideoCreatedEvent videoCreate = new CourseVideoCreatedEvent(
                video.Id, video.Title, video.CourseId, video.IsFree,
                video.IsPublished, video.ThumbnailImageName, video.Priority,
                video.VideoFileName, video.VideoLength);

            await _mediatorHandler.PublishEvent(videoCreate);

            var course = await _courseRepository.GetByIdAsync(video.CourseId);
            course.UpdatedDateOnUtc = DateTime.UtcNow;

            _courseRepository.Update(course);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<int>> DeleteCourseVideoById(int id)
        {
            if (id < 1) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            var video = await _videoRepository.GetByIdAsync(id);
            if (video is null) return Result.Failure<int>(ErrorMessages.FailedOperationError);

            video.IsDeleted = true;

            _videoRepository.Update(video);
            await _videoRepository.SaveAsync();

            return video.CourseId;
        }

        public async Task<Result<AdminSideUpdateCourseVideoViewModel>> GetCourseVideoForEdit(int id)
        {
            if (id < 1) return Result.Failure<AdminSideUpdateCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            var video = await _videoRepository.FirstOrDefaultAsync(n => n.Id == id, null, null, "VideoCategory");

            if (video is null) return Result.Failure<AdminSideUpdateCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            AdminSideUpdateCourseVideoViewModel model = new AdminSideUpdateCourseVideoViewModel().MapFrom(video);

            await _localizingService.FillModelToEditByAdminAsync(video, model);

            return model;
        }

        public async Task<Result> UpdateCourseVideo(AdminSideUpdateCourseVideoViewModel model)
        {
            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            CourseVideo video = await _videoRepository.GetByIdAsync(model.Id, "VideoCategory");
            if (video is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (video.Priority != model.Priority && await _courseRepository.IsOrderDuplicated(model.Priority, model.CourseId)) return Result.Failure(ErrorMessages.CourseVideoOrderIsDuplicated);

            var copy = video.DeepCopy<CourseVideo>();

            if (model.VideoLength.Length != 8) return Result.Failure(ErrorMessages.InvalidVideoLengthError);

            var courseVideoLength = new TimeSpan();
            try
            {
                var timesList = model.VideoLength.Split(":").ToList();
                courseVideoLength = new TimeSpan(Convert.ToInt32(timesList[0]), Convert.ToInt32(timesList[1]), Convert.ToInt32(timesList[2]));

                if (courseVideoLength > new TimeSpan(23, 59, 59) || courseVideoLength < new TimeSpan(00, 00, 01)) return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }
            catch
            {
                return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }

            if (await _videoRepository.AnyAsync(n => n.CourseId == model.CourseId && n.Id != model.Id && n.Priority == model.Priority)) return Result.Failure(ErrorMessages.DuplicateSessionNumberError);

            //if (model.VideoThumbnailImage is not null)
            //{
            //    if (model.VideoThumbnailImage.Length > 10_000_000) return Result.Failure(ErrorMessages.ImageSizeError);

            //    if (!model.VideoThumbnailImage.ContentType.Contains("image/")) return Result.Failure(ErrorMessages.FileTypeError);

            //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.VideoThumbnailImage.FileName);
            //    string filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(model.CourseId);

            //    video.ThumbnailImageName.DeleteImage(filePath);

            //    model.VideoThumbnailImage.AddImageToServer(fileName, filePath, null, null);
            //    video.ThumbnailImageName = fileName;
            //}

            video.Title = model.Title;
            video.VideoLength = courseVideoLength;
            video.Priority = model.Priority;
            video.IsFree = model.IsFree;
            video.IsPublished = model.IsPublished;
            video.VideoFileName = model.VideoFileName;
            video.AttachFileName = model.AttachFileName;

            if (video.VideoCategory.Title != model.CourseVideoCategoryTitle)
            {
                if (await _videoCategoryRepository.AnyAsync(n => n.Title == model.CourseVideoCategoryTitle))
                {
                    video.VideoCategoryId = await _videoCategoryRepository.GetVideoCategoryIdByTitle(model.CourseVideoCategoryTitle);
                }
                else
                {
                    video.VideoCategory = new CourseVideoCategory
                    {
                        Title = model.CourseVideoCategoryTitle,
                        CourseId = model.CourseId
                    };
                }
            }

            _videoRepository.Update(video);
            await _videoRepository.SaveAsync();

            await _localizingService.SaveLocalizations(video, model);

            CourseVideoUpdatedEvent updateVideo = new CourseVideoUpdatedEvent(
                copy.Id, copy.Title, copy.CourseId, copy.IsFree, copy.IsPublished,
                copy.ThumbnailImageName, copy.Priority, copy.VideoFileName, copy.VideoLength,
                video.Title, video.CourseId, video.IsFree, video.IsPublished, video.ThumbnailImageName,
                video.Priority, video.VideoFileName, video.VideoLength);

            await _mediatorHandler.PublishEvent(updateVideo);

            var course = await _courseRepository.GetByIdAsync(video.CourseId);
            course.UpdatedDateOnUtc = DateTime.UtcNow;

            _courseRepository.Update(course);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<FilterUserPanelCourseVideoViewModel>> UserPanelFilterCourseVideos(FilterUserPanelCourseVideoViewModel filter)
        {
            if (filter is null) return Result.Failure<FilterUserPanelCourseVideoViewModel>(ErrorMessages.FailedOperationError);
            if (string.IsNullOrWhiteSpace(filter.slug)) return Result.Failure<FilterUserPanelCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            var CourseId = await _courseRepository.GetCourseIdBySlug(filter.slug);

            if (CourseId < 1) return Result.Failure<FilterUserPanelCourseVideoViewModel>(ErrorMessages.FailedOperationError);
            var masterId = await _courseRepository.GetCourseMasterIdByCourseId(CourseId);

            if (masterId != _httpContextAccessor.HttpContext.User.GetUserId()) return Result.Failure<FilterUserPanelCourseVideoViewModel>(ErrorMessages.FailedOperationError);

            var conditions = Filter.GenerateConditions<CourseVideo>();

            conditions.Add(n => n.CourseId == CourseId);

            await _videoRepository.FilterAsync(filter, conditions, CourseVideoMapper.MapCourseVideosForMaster);

            await _localizingService.TranslateModelAsync(filter.Entities);

            return filter;
        }

        public async Task<Result> CreateNewCourseVideo(UserPanelCreateNewCourseVideoViewModel model)
        {
            if (await _courseRepository.IsOrderDuplicated(model.Priority, model.CourseId)) return Result.Failure(ErrorMessages.CourseVideoOrderIsDuplicated);

            if (model is null) return Result.Failure(ErrorMessages.FailedOperationError);

            if (!await _courseRepository.AnyAsync(n => n.Id == model.CourseId)) return Result.Failure(ErrorMessages.FailedOperationError);


            if (model.VideoLength.Length != 8) return Result.Failure(ErrorMessages.InvalidVideoLengthError);

            var courseVideoLength = new TimeSpan();
            try
            {
                var timesList = model.VideoLength.Split(":").ToList();
                courseVideoLength = new TimeSpan(Convert.ToInt32(timesList[0]), Convert.ToInt32(timesList[1]), Convert.ToInt32(timesList[2]));

                if (courseVideoLength > new TimeSpan(23, 59, 59) || courseVideoLength < new TimeSpan(00, 00, 01)) return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }
            catch
            {
                return Result.Failure(ErrorMessages.InvalidVideoLengthError);
            }



            //if (model.VideoThumbnailImage.Length > 10_000_000) return Result.Failure(ErrorMessages.ImageSizeError);

            //if (!model.VideoThumbnailImage.ContentType.Contains("image/")) return Result.Failure(ErrorMessages.FileTypeError);

            //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.VideoThumbnailImage.FileName);
            string filePath = SiteTools.CourseDetailsFiles.GetCourseDetailsFilesPath(model.CourseId);

            //model.VideoThumbnailImage.AddImageToServer(fileName, filePath, null, null);

            var masterId = await _courseRepository.GetCourseMasterIdByCourseId(model.CourseId);
            if (masterId != _httpContextAccessor.HttpContext.User.GetUserId()) return Result.Failure(ErrorMessages.FailedOperationError);

            CourseVideo video = new CourseVideo()
            {
                CourseId = model.CourseId,
                IsFree = model.IsFree,
                IsPublished = false,
                Title = model.Title,
                //ThumbnailImageName = fileName,
                Priority = model.Priority,
                VideoFileName = model.VideoFileName,
                VideoLength = courseVideoLength,
                AttachFileName = model.AttachFileName
            };

            if (await _videoCategoryRepository.AnyAsync(n => n.Title == model.CourseVideoCategoryTitle))
            {
                video.VideoCategoryId = await _videoCategoryRepository.GetVideoCategoryIdByTitle(model.CourseVideoCategoryTitle);
            }
            else
            {
                video.VideoCategory = new CourseVideoCategory()
                {
                    Title = model.CourseVideoCategoryTitle,
                    CourseId = model.CourseId
                };
            }

            await _videoRepository.InsertAsync(video);
            await _videoRepository.SaveAsync();

            CourseVideoCreatedEvent videoCreate = new CourseVideoCreatedEvent(
                video.Id, video.Title, video.CourseId, video.IsFree,
                video.IsPublished, video.ThumbnailImageName, video.Priority,
                video.VideoFileName, video.VideoLength);

            await _mediatorHandler.PublishEvent(videoCreate);

            var course = await _courseRepository.GetByIdAsync(video.CourseId);
            course.UpdatedDateOnUtc = DateTime.UtcNow;

            _courseRepository.Update(course);
            await _courseRepository.SaveAsync();

            return Result.Success();
        }

        public async Task<Result<bool>> PublishCourseVideoAsync(int id)
        {
            if (id <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.NullValue);
            }

            var result = await _videoRepository.PublishCourseVideoAsync(id);

            if (result <= 0)
            {
                return Result.Failure<bool>(ErrorMessages.FailedOperationError);
            }

            return Result.Success(true, SuccessMessages.SuccessfullyDone);
        }
    }

    #endregion
}