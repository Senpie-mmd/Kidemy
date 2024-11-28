using Kidemy.Application.ViewModels.Survey.SurveyAnswersModels;
using Kidemy.Application.ViewModels.Survey.SurveyModels;
using Kidemy.Application.ViewModels.Survey.SurveyQuestionModels;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Models.Survey;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class SurveyMapper
    {

        public static Expression<Func<Survey, AdminSideSurveyViewModel>> MapAdminSideSurveyViewModel => (Survey survey) => new AdminSideSurveyViewModel
        {
            Id = survey.Id,
            IsPublished = survey.IsPublished,
            Title = survey.Title
        };

        public static AdminSideUpsertSurveyViewModel MapFrom(this AdminSideUpsertSurveyViewModel model, Survey survey)
        {
            model.Id = survey.Id;
            model.Title = survey.Title;
            model.IsPublished = survey.IsPublished;

            return model;
        }

        public static Expression<Func<SurveyQuestion, AdminSideSurveyQuestionViewModel>> MapAdminSideSurveyQuestionViewModel => (SurveyQuestion surveyQuestion) => new AdminSideSurveyQuestionViewModel
        {
            Id = surveyQuestion.Id,
            SurveyId = surveyQuestion.SurveyId,
            Title = surveyQuestion.Title,
            Type = surveyQuestion.Type,
            Options = surveyQuestion.Options,
            Priority = surveyQuestion.Priority
        };

        public static AdminSideUpsertSurveyQuestionViewModel MapFrom(this AdminSideUpsertSurveyQuestionViewModel model, SurveyQuestion surveyQuestion)
        {
            model.Id = surveyQuestion.Id;
            model.SurveyId = surveyQuestion.SurveyId;
            model.Title = surveyQuestion.Title;
            model.Type = surveyQuestion.Type;
            model.Options = surveyQuestion.Options;
            model.Priority = surveyQuestion.Priority;

            return model;
        }

        public static ClientSideSurveyViewModel MapFrom(this ClientSideSurveyViewModel model, Survey survey)
        {
            model.Id = survey.Id;
            model.Title = survey.Title;
            model.IsPublished = survey.IsPublished;
            model.Questions = survey.Questions.Select(question => new ClientSideSurveyQuestionViewModel().MapFrom(question)).ToList();

            return model;
        }

        public static ClientSideSurveyQuestionViewModel MapFrom(this ClientSideSurveyQuestionViewModel model, SurveyQuestion surveyQuestion)
        {
            model.Id = surveyQuestion.Id;
            model.SurveyId = surveyQuestion.SurveyId;
            model.Title = surveyQuestion.Title;
            model.Type = surveyQuestion.Type;
            model.Options = surveyQuestion.Options;
            model.Priority = surveyQuestion.Priority;

            return model;
        }

        public static Expression<Func<SurveyAnswersReportModel, AdminSideSurveyAnswerViewModel>> MapAdminSideSurveyAnswerViewModel => (SurveyAnswersReportModel surveyAnswer) => new AdminSideSurveyAnswerViewModel
        {
            SurveyId = surveyAnswer.SurveyId,
            AnswererId = surveyAnswer.AnswererId,
            CreatedOnUtc = surveyAnswer.CreatedDateOnUtc
        };

        public static AdminSideSurveyAnswerDetailsViewModel MapFrom(this AdminSideSurveyAnswerDetailsViewModel model, List<SurveyAnswer> surveyAnswers)
        {
            model.SurveyId = surveyAnswers.First().Question.SurveyId;
            model.AnswererId = surveyAnswers.First().AnswererId;
            model.SurveyTitle = surveyAnswers.First().Question.Survey.Title;
            model.Answers = surveyAnswers.Select(answer => new AdminSideSurveyAnswerQuestionViewModel().MapFrom(answer)).ToList();

            return model;
        }

        public static AdminSideSurveyAnswerQuestionViewModel MapFrom(this AdminSideSurveyAnswerQuestionViewModel model, SurveyAnswer surveyAnswer)
        {
            model.QuestionTitle = surveyAnswer.Question.Title;
            model.QuestionId = surveyAnswer.QuestionId;
            model.AnswerText = surveyAnswer.AnswerText;
         
            return model;
        }
    }
}
