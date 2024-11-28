using Kidemy.Application.ViewModels.Course.AdminSideCourse.CourseQuestionAnswer;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestion;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.CourseQuestionAnswer;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Models.Course;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class CourseQuestionAnswerMapper
    {
        public static Expression<Func<CourseQuestionAnswer, ClientSideCourseQuestionAnswerViewModel>> MapClientSideCourseQuestionAnswerViewModel => (CourseQuestionAnswer courseQuestionAnswer) =>
        new ClientSideCourseQuestionAnswerViewModel()
        {
            Id = courseQuestionAnswer.Id,
            AnsweredById = courseQuestionAnswer.AnsweredById,
            Answer = courseQuestionAnswer.Answer,
            CreateDate = courseQuestionAnswer.CreatedDateOnUtc,
            QuestionId = courseQuestionAnswer.QuestionId,
            QuestionTitle = courseQuestionAnswer.Question.Title,
            CourseTitle = courseQuestionAnswer.Question.Course.Title,
            CourseSlug = courseQuestionAnswer.Question.Course.Slug,
            IsConfirmed = courseQuestionAnswer.IsConfirmed,

        };

        public static ClientSideCourseQuestionAnswerViewModel MapFrom(this ClientSideCourseQuestionAnswerViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AnsweredById = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.UserProfile = userInfo.UserAvatar;

            return model;
        }

        public static Expression<Func<CourseQuestionAnswer, AdminSideCourseQuestionAnswerViewModel>> MapAdminSideCourseQuestionAnswerViewModel => (CourseQuestionAnswer courseQuestionAnswer) =>
       new AdminSideCourseQuestionAnswerViewModel()
       {
           Id = courseQuestionAnswer.Id,
           AnsweredById = courseQuestionAnswer.AnsweredById,
           Answer = courseQuestionAnswer.Answer,
           CreateDate = courseQuestionAnswer.CreatedDateOnUtc,
           QuestionId = courseQuestionAnswer.QuestionId,
           IsConfirmed = courseQuestionAnswer.IsConfirmed,
       };

        public static AdminSideCourseQuestionAnswerViewModel MapFromAdmin(this AdminSideCourseQuestionAnswerViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AnsweredById = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.UserProfile = userInfo.UserAvatar;

            return model;
        }
    }
}
