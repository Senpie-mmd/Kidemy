using Kidemy.Application.Convertors;
using Kidemy.Application.ViewModels.Course;
using Kidemy.Application.ViewModels.Course.AdminSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Categories;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Comments;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseDetail;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseTag;
using Kidemy.Application.ViewModels.Course.UserPanel;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs.Course;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Interfaces.ContactUs;
using Kidemy.Domain.Models.Course;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseMapper
    {
        public static ClientSideCategoryViewModel MapFrom(this ClientSideCategoryViewModel model, CategoryModel category)
        {
            model.Title = category.Title;
            model.imageName = category.logoFileName;
            model.CoursesCount = category.CourseCount;

            return model;
        }

        public static CourseCategoryFullDetailsViewModel MapFrom(this CourseCategoryFullDetailsViewModel model, CourseCategory category)
        {
            model.Id = category.Id;
            model.Title = category.Title;
            model.ParentCourseCategoryId = category.ParentCourseCategoryId;

            return model;
        }

        public static CourseFullDetailsViewModel MapFrom(this CourseFullDetailsViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Title = course.Title;
            model.ShortDescription = course.ShortDescription;
            model.Description = course.Description;
            model.Slug = course.Slug;
            model.Level = course.Level;
            model.Status = course.Status;
            model.IsOffer = course.IsOffer;
            model.MasterId = course.MasterId;
            model.Price = course.Price;
            model.VideosTotalTime = course.VideosTotalTime;
            model.HasCertificate = course.HasCertificate;
            model.AllowComenting = course.AllowComenting;
            model.Type = course.Type;
            model.IsPublished = course.IsPublished;
            model.ImageName = course.ImageName;
            model.DemoVideoFileName = course.DemoVideoFileName;
            model.Categories = course.Categories?.Select(category => new CourseCategoryFullDetailsViewModel().MapFrom(category.Category)).ToList() ?? new List<CourseCategoryFullDetailsViewModel>();

            return model;
        }

        public static CourseShortDetailsViewModel MapFrom(this CourseShortDetailsViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Title = course.Title;
            model.Slug = course.Slug;
            model.Price = course.Price ?? 0;
            model.ImageFileName = course.ImageName;

            return model;
        }

        public static Expression<Func<Course, ClientSideCourseViewModel>> MapCourseViewModel => (Course course) => new ClientSideCourseViewModel
        {
            Id = course.Id,
            Status = course.Status,
            Price = course.Price,
            Description = course.ShortDescription,
            Level = course.Level,
            Title = course.Title,
            ImageFileName = course.ImageName,
            MasterId = course.MasterId,
            Slug = course.Slug,
            Type = course.Type,
            AvrageScore = course.Comments.Where(n => n.IsConfirmed == true && n.ReplyCommnetId == null).Select(n => (int)n.Score).ToList().GetAvrage(),
            VideosCount = course.Videos.Where(n => n.IsPublished == true).Count(),
            CourseVideosLength = course.Videos.Where(n => n.IsPublished).Select(n => n.VideoLength).ToList()
        };
        public static Expression<Func<Course, ClientSideCourseForListViewModel>> MapUserPanelCourseViewModel => (Course course) => new ClientSideCourseForListViewModel
        {
            Id = course.Id,
            Title = course.Title,
            ImageName = course.ImageName,
            Slug = course.Slug,
            Type = course.Type,
            UpdatedDateOnUTC = course.UpdatedDateOnUtc
        };

        public static ClientSideHomePageOfferedCoursesViewModel MapFrom(this ClientSideHomePageOfferedCoursesViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Slug = course.Slug;
            model.ImageFileName = course.ImageName;
            model.Level = course.Level;
            model.CategoryId = course.Categories.FirstOrDefault()?.CategoryId ?? 0;
            model.Title = course.Title;
            model.Score = course.Comments.Where(n => n.IsConfirmed && n.ReplyCommnetId == null).Select(n => (int)n.Score).ToList().GetAvrage();
            model.CommentsCount = course.Comments.Where(n => n.IsConfirmed && n.ReplyCommnetId == null).Count();
            model.StudentsCount = course.Users.Count();
            model.VideosLengths = course.Videos.Where(n => n.IsPublished).Select(n => n.VideoLength).ToList();
            model.VideosCount = course.Videos.Where(n => n.IsPublished).Count();
            model.MasterId = course.MasterId;
            model.Price = course.Price;
            model.Type = course.Type;

            return model;
        }

        public static AdminSideCourseViewModel MapFrom(this AdminSideCourseViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Status = course.Status;
            model.Price = course.Price;
            model.Level = course.Level;
            model.Title = course.Title;
            model.MasterId = course.MasterId;
            model.Slug = course.Slug;
            model.CourseType = course.Type;

            return model;
        }

        public static Expression<Func<CourseComment, ClientSideCommentViewModel>> MapCommentViewModel = (CourseComment comment) => new ClientSideCommentViewModel
        {
            Id = comment.Id,
            Score = comment.Score,
            Message = comment.Message,
            CommentedDate = comment.CreatedDateOnUtc.ToLocalTime(),
            UserId = comment.CommentedById,
            RepliedComments = comment.RepliedComments.Where(n => n.IsConfirmed == true).Select(n => new ClientSideCommentViewModel()
            {
                Id = n.Id,
                CommentedDate = n.CreatedDateOnUtc.ToLocalTime(),
                Score = n.Score,
                Message = n.Message,
                UserId = n.CommentedById,
                ReplyForCommentId = comment.Id
            }).ToList()
        };

        public static Expression<Func<Course, AdminSideCourseViewModel>> MapCourseViewModelAdminSide => (course) => new AdminSideCourseViewModel
        {
            Id = course.Id,
            Status = course.Status,
            Price = course.Price,
            Level = course.Level,
            Title = course.Title,
            MasterId = course.MasterId,
            Slug = course.Slug,
            CourseType = course.Type
        };

        public static CategoriesForCourseListClientSideViewModel MapFrom(this CategoriesForCourseListClientSideViewModel model, CategoryModel category)
        {
            model.Id = category.Id;
            model.Title = category.Title;
            model.CourseCount = category?.CourseCount ?? 0;
            model.ParentId = category.ParentCourseCategoryId;

            return model;
        }

        public static ClientSideCourseTagsViewModel MapFrom(this ClientSideCourseTagsViewModel model, CourseTagsModel tag)
        {
            model.Id = tag.Id;
            model.Title = tag.Title;

            return model;
        }

        public static ClientSideCourseWithVideosViewModel MapFrom(this ClientSideCourseWithVideosViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Type = course.Type;
            model.Videos = course.Videos?.Select(n => new ClientSideCourseVideoViewModel().MapFrom(n))?.ToList();

            return model;
        }

        public static ClientSideCourseDetailViewModel MapFrom(this ClientSideCourseDetailViewModel model, CourseDetailsModel course)
        {
            model.Id = course.Id;
            model.ImageFileName = course.ImageFileName;
            model.DemoVideoFileName = course.DemoVideoFileName;
            model.CategoryName = course.CategoryName;
            model.Title = course.Title;
            model.ShortDescription = course.ShortDescription;
            model.Description = course.Description;
            model.Level = course.Level;
            model.LastUpdatedDate = course.LastUpdatedDate;
            model.VideoCount = course.VideoCount;
            model.CourseVideosTotalTime = new TimeSpan(course.CourseVideosTotalTime.Sum(n => n.Ticks));
            model.HasCertificate = course.HasCertificate;
            model.Slug = course.Slug;
            model.Price = course.Price;
            model.Type = course.Type;
            model.Tags = course.Tags.Select(n => new ClientSideCourseTagsViewModel().MapFrom(n)).ToList();
            model.AvrageScore = course.CourseScore.Select(n => (int)n).ToList().GetAvrage();
            model.MasterId = course.MasterId;

            return model;
        }

        public static ClientSideCourseVideoViewModel MapFrom(this ClientSideCourseVideoViewModel model, CourseVideo video)
        {
            model.Id = video.Id;
            model.ThumbnailImageName = video.ThumbnailImageName;
            model.Title = video.Title;
            model.VideoLength = video.VideoLength;
            model.Priority = video.Priority;
            model.IsPublished = video.IsPublished;
            model.HasAttachmentFile = !string.IsNullOrWhiteSpace(video.AttachFileName);
            model.VideoCategoryTitle = video.VideoCategory.Title;
            model.IsFree = video.IsFree;

            return model;
        }

        public static ClientSideCourseCategoryVideosViewModel MapFrom(this ClientSideCourseCategoryVideosViewModel model, CourseVideosCategoryModel videoCategory)
        {
            model.Title = videoCategory.Title;
            model.VideosCount = videoCategory.VideoCount;
            model.VideosWithDetails = videoCategory.Videos.Select(n => new ClientSideCourseVideoViewModel().MapFrom(n)).ToList();
            return model;
        }

        public static CourseCategoryWithSubCategoriesViewModel MapFrom(this CourseCategoryWithSubCategoriesViewModel model, CourseCategory courseCategory)
        {
            model.Id = courseCategory.Id;
            model.Title = courseCategory.Title;
            model.ParentCourseCategoryId = courseCategory.ParentCourseCategoryId;
            if (courseCategory.SubCategories?.Any() ?? false)
            {
                model.SubCategories = courseCategory.SubCategories.Select(n => new CourseCategoryWithSubCategoriesViewModel().MapFrom(n)).ToList();
            }

            return model;
        }

        public static ClientSideCommentViewModel MapFrom(this ClientSideCommentViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.UserProfile = userInfo.UserAvatar;

            return model;
        }

        public static AdminSideCategoriesForCourseListViewModel MapFrom(this AdminSideCategoriesForCourseListViewModel model, CategoryModel category)
        {
            model.Title = category.Title;
            model.CourseCount = category?.CourseCount ?? 0;
            model.ParentId = category.ParentCourseCategoryId;

            return model;
        }

        public static AdminSideUpdateCourseViewModel MapFrom(this AdminSideUpdateCourseViewModel model, Course course, string masterFullName)
        {
            model.Id = course.Id;
            model.Title = course.Title;
            model.ShortDescription = course.ShortDescription;
            model.Description = course.Description;
            model.Slug = course.Slug;
            model.CourseTags = string.Join(",", course.CourseTags.Select(n => n.Tag.Title).ToList());
            model.Level = course.Level;
            model.Status = course.Status;
            model.IsOffer = course.IsOffer;
            model.MasterId = course.MasterId;
            model.Price = course.Price;
            model.MasterCommissionPercentage = course.MasterCommissionPercentage;
            model.FileSuffix = course.FileSuffix;
            model.HasCertificate = course.HasCertificate;
            model.AllowComenting = course.AllowComenting;
            model.Type = course.Type;
            model.IsPublished = course.IsPublished;
            model.DemoVideoFileName = course.DemoVideoFileName;
            model.MasterFullName = masterFullName;
            model.CourseCategoryIds = course.Categories.Select(n => n.CategoryId).ToList();
            model.UploadedCourseImage = course.ImageName;

            return model;
        }

        public static ClientSideCourseTagViewModel MapFrom(this ClientSideCourseTagViewModel model, CourseTag courseTag)
        {
            model.Id = courseTag.Id;
            model.Title = courseTag.Title;

            return model;
        }

        public static Expression<Func<Course, UserPanelCourseViewModel>> MapCoursesListForMaster => (course) => new UserPanelCourseViewModel
        {
            Id = course.Id,
            Episodes = course.Videos.Count(),
            Status = course.Status,
            Title = course.Title,
            Slug = course.Slug,
            ImageName = course.ImageName,
            Price = course.Price
        };

        public static ClientSideOfferedCoursesViewModel MapFrom(this ClientSideOfferedCoursesViewModel model, Course course, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.Id = course.Id;
            model.CourseImage = course.ImageName;
            model.MasterId = course.MasterId;
            model.CourseTitle = course.Title;
            model.CoursePrice = course.Price;
            model.CourseType = course.Type;
            model.CategoryName = course.Categories.FirstOrDefault().Category.Title;
            model.Slug = course.Slug;
            model.ImageName = course.ImageName;
            model.MasterFullName = userInfo.UserName;
            model.MasterAvatarFileName = userInfo.UserAvatar;


            return model;
        }

        public static ClientSideMastersOtherCoursesViewModel MapFrom(this ClientSideMastersOtherCoursesViewModel model, Course course, UserNameAndUserProfileModel? userInfo)
        {
            model.Id = course.Id;
            model.slug = course.Slug;
            model.ImageFileName = course.ImageName;
            model.Type = course.Type;
            model.MasterId = course.MasterId;
            model.MasterAvatarFileName = userInfo.UserAvatar;
            model.Score = course.Comments.Where(n => n.IsConfirmed == true && n.ReplyCommnetId is null).Select(n => (int)n.Score).ToList().GetAvrage();
            model.Students = course.Users?.Count() ?? 0;
            model.Title = course.Title;
            model.CategoryTitle = course.Categories?.FirstOrDefault()?.Category.Title ?? "-";
            model.CategoryId = course.Categories?.FirstOrDefault()?.CategoryId ?? 0;
            model.Price = course.Price;

            return model;
        }

        public static ClientSideCourseViewModel MapFrom(this ClientSideCourseViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Status = course.Status;
            model.Price = course.Price;
            model.Description = course.ShortDescription;
            model.Level = course.Level;
            model.Title = course.Title;
            model.ImageFileName = course.ImageName;
            model.MasterId = course.MasterId;
            model.Slug = course.Slug;
            model.Type = course.Type;
            model.AvrageScore = course.Comments
                                      ?.Where(n => n.ReplyCommnetId == null && n.IsConfirmed == true)
                                      ?.Select(n => (int)n.Score)
                                      ?.ToList()
                                      ?.GetAvrage();
            model.VideosCount = course?.Videos?.Where(n => n.IsPublished)?.Count();
            model.CourseVideosLength = course?.Videos?.Where(n => n.IsPublished)?.Select(n => n.VideoLength)?.ToList();

            return model;
        }

        public static ClientSideLastCoursesViewModel MapFrom(this ClientSideLastCoursesViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Slug = course.Slug;
            model.Title = course.Title;
            model.ImageFileName = course.ImageName;
            model.Price = course.Price;
            model.Type = course.Type;
            model.AvrageScore = course.Comments.Where(n => n.IsConfirmed && n.ReplyCommnetId is null).Select(n => (int)n.Score).ToList().GetAvrage();

            return model;
        }

        public static ClientSideCourseShortDetailsViewModel MapFrom(this ClientSideCourseShortDetailsViewModel model, Course course)
        {
            model.Id = course.Id;
            model.Status = course.Status;
            model.Price = course.Price;
            model.Description = course.ShortDescription;
            model.Level = course.Level;
            model.Title = course.Title;
            model.ImageFileName = course.ImageName;
            model.MasterId = course.MasterId;
            model.Slug = course.Slug;
            model.Type = course.Type;

            return model;
        }
    }
}
