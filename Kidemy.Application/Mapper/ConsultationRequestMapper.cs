using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using Kidemy.Application.ViewModels.Consultation.ConsultationRequest;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.Models.Consultation;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class ConsultationRequestMapper
    {

        public static Expression<Func<ConsultationRequest, ClientSideConsultaionRequestViewModel>> MapClientSideConsultationRequest => (ConsultationRequest consultationRequest) =>
   new ClientSideConsultaionRequestViewModel()
   {
       Id = consultationRequest.Id,
       state = consultationRequest.State,
       Topic = consultationRequest.Topic,
       Description = consultationRequest.Description,
       FixedTime = consultationRequest.FixedDate,
       DayOfWeek = consultationRequest.SelectedDate.DayOfWeek,
       StartTime = consultationRequest.SelectedDate.StartTime,
       EndTime = consultationRequest.SelectedDate.EndTime,
       TypeTitle = consultationRequest.AdviserConsultationType.Title,
       TypePrice = consultationRequest.AdviserConsultationType.Price,
       AdviserUserId=consultationRequest.Adviser.UserId,
   };

        public static Expression<Func<ConsultationRequest, AdminSideConsultaionRequestViewModel>> MapAdminSideConsultationRequest => (ConsultationRequest consultationRequest) =>
   new AdminSideConsultaionRequestViewModel()
   {
       Id = consultationRequest.Id,
       state = consultationRequest.State,
       Topic = consultationRequest.Topic,
       Description = consultationRequest.Description,
       FixedTime = consultationRequest.FixedDate,
       DayOfWeek = consultationRequest.SelectedDate.DayOfWeek,
       StartTime = consultationRequest.SelectedDate.StartTime,
       EndTime = consultationRequest.SelectedDate.EndTime,
       TypeTitle = consultationRequest.AdviserConsultationType.Title,
       TypePrice = consultationRequest.AdviserConsultationType.Price,
       AdviserUserId = consultationRequest.Adviser.UserId,
       RequestedByUserId=consultationRequest.RequestedByUserId,
   };

        public static ClientSideConsultaionRequestViewModel MapFrom(this ClientSideConsultaionRequestViewModel model, ConsultationRequest consultationRequest)
        {
            model.Id = consultationRequest.Id;
            model.state = consultationRequest.State;
            model.FixedTime = consultationRequest.FixedDate;
            model.Topic = consultationRequest.Topic;
            model.Description = consultationRequest.Description;
            model.DayOfWeek = consultationRequest.SelectedDate.DayOfWeek;
            model.StartTime = consultationRequest.SelectedDate.StartTime;
            model.EndTime = consultationRequest.SelectedDate.EndTime;
            model.TypeTitle = consultationRequest.AdviserConsultationType.Title;
            model.TypePrice = consultationRequest.AdviserConsultationType.Price;
            return model;
        }


        public static AdminSideConsultaionRequestViewModel MapFrom(this AdminSideConsultaionRequestViewModel model, ConsultationRequest consultationRequest)
        {
            model.Id = consultationRequest.Id;
            model.state = consultationRequest.State;
            model.FixedTime = consultationRequest.FixedDate;
            model.Topic = consultationRequest.Topic;
            model.Description = consultationRequest.Description;
            model.DayOfWeek = consultationRequest.SelectedDate.DayOfWeek;
            model.StartTime = consultationRequest.SelectedDate.StartTime;
            model.EndTime = consultationRequest.SelectedDate.EndTime;
            model.TypeTitle = consultationRequest.AdviserConsultationType.Title;
            model.TypePrice = consultationRequest.AdviserConsultationType.Price;
            return model;
        }


        public static ClientSideConsultaionRequestViewModel MapFrom(this ClientSideConsultaionRequestViewModel model, ClientSideUserNameAndUserProfileViewModel  userInfo)
        {
            model.AdviserUserName = userInfo.UserName;

            return model;
        }
        public static AdminSideConsultaionRequestViewModel MapFrom(this AdminSideConsultaionRequestViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.AdviserUserName =  userInfo.UserName ?? "-"; 
            return model;
        }
        public static AdminSideConsultaionRequestViewModel MapFromUserId(this AdminSideConsultaionRequestViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.RequestedByUserName = userInfo.UserName ?? "-";
            return model;
        }
    }
}
