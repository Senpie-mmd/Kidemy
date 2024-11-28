using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs.User;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseQuestionMapper
    {
        public static Expression<Func<CourseQuestion, ClientSideCourseQuestionViewModel>> MapClientSideCourseQuestionViewModel => (CourseQuestion courseQuestion) =>
          new ClientSideCourseQuestionViewModel()
          {
              Id = courseQuestion.Id,
              Title = courseQuestion.Title,
              Description = courseQuestion.Description,
              IsClosed = courseQuestion.IsClosed,
              IsConfirmed = courseQuestion.IsConfirmed,
              CreateDate = courseQuestion.CreatedDateOnUtc,
              AskedById = courseQuestion.AskedById,
              CourseTitle= courseQuestion.Course.Title,
              CourseSlug= courseQuestion.Course.Slug,
          };
        public static Expression<Func<CourseQuestion, AdminSideCourseQuestionViewModel>> MapAdminSideCourseQuestionViewModel => (CourseQuestion courseQuestion) =>
        new AdminSideCourseQuestionViewModel()
        {
            Id = courseQuestion.Id,
            Title = courseQuestion.Title,
            Description = courseQuestion.Description,
            IsClosed = courseQuestion.IsClosed,
            IsConfirmed = courseQuestion.IsConfirmed,
            CreateDate = courseQuestion.CreatedDateOnUtc,
            AskedById = courseQuestion.AskedById,
            CourseTitle = courseQuestion.Course.Title,
            MasterId=courseQuestion.Course.MasterId,
        };

        public static ClientSideCourseQuestionViewModel MapFrom(this ClientSideCourseQuestionViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AskedById = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.UserProfile = userInfo.UserAvatar;

            return model;
        }
        public static AdminSideCourseQuestionViewModel MapFrom(this AdminSideCourseQuestionViewModel model, AdminSideUserNameAndMobileForMasterModel userInfo)
        {
            model.AskedById = userInfo.Id;
            model.MasterFirstName = userInfo.UserFirstName; 
            model.MasterLastName = userInfo.UserLastName;

            return model;
        }

        public static ClientSideCourseQuestionViewModel MapFrom(this ClientSideCourseQuestionViewModel model, CourseQuestion courseQuestion)
        {
            model.Description = courseQuestion.Description;
            model.AskedById = courseQuestion.AskedById;
            model.CourseId = courseQuestion.CourseId;
            model.CreateDate = courseQuestion.CreatedDateOnUtc;
            model.IsConfirmed = courseQuestion.IsConfirmed;
            model.IsClosed = courseQuestion.IsClosed;
            model.Title = courseQuestion.Title;
            model.Id = courseQuestion.Id;
            
            return model;
        }

    }
}
