using Kidemy.Application.ViewModels.BankAccountCard;
using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Application.ViewModels.Consultation.AdviserAvailableDate;
using Kidemy.Application.ViewModels.Consultation.AdviserConsultationType;
using Kidemy.Application.ViewModels.User.ClientSide;
using Kidemy.Domain.DTOs.Master;
using Kidemy.Domain.Models.BankAccountCard;
using Kidemy.Domain.Models.Consultation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class AdviserMapper
    {
        public static Expression<Func<Adviser, AdminSideAdviserViewModel>> MapAdminSideAdviserViewModel => (Adviser adviser) =>
    new AdminSideAdviserViewModel()
    {
        Id = adviser.Id,
        IsPublished = adviser.IsPublished,
        Priority = adviser.Priority,
        UserId = adviser.UserId,
        ConsultationPercentage = adviser.ConsultationPercentage,
       
    };
        public static Expression<Func<Adviser, ClientSideAdviserViewModel>> MapClientSideAdviserViewModel => (Adviser adviser) =>
new ClientSideAdviserViewModel()
{
    Id = adviser.Id,
    IsPublished = adviser.IsPublished,
    Priority = adviser.Priority,
    UserId = adviser.UserId,
    ConsultationPercentage = adviser.ConsultationPercentage,

};

        public static AdminSideAdviserAvailableDateViewModel MapFrom(this AdminSideAdviserAvailableDateViewModel model, AdviserAvailableDate availableDate)
        {
            model.Id = availableDate.Id;
            model.StartTime = availableDate.StartTime;
            model.EndTime = availableDate.EndTime;
            model.DayOfWeek = availableDate.DayOfWeek;
            model.AdviserId = availableDate.AdviserId;
            return model;
        }
        public static ClientSideAdviserAvailableDateViewModel MapFrom(this ClientSideAdviserAvailableDateViewModel model, AdviserAvailableDate availableDate)
        {
            model.Id = availableDate.Id;
            model.StartTime = availableDate.StartTime;
            model.EndTime = availableDate.EndTime;
            model.DayOfWeek = availableDate.DayOfWeek;
            model.AdviserId = availableDate.AdviserId;
            return model;
        }
        public static AdminSideAdviserConsultationTypeViewModel MapFrom(this AdminSideAdviserConsultationTypeViewModel model, AdviserConsultationType adviserConsultationType)
        {
            model.Id = adviserConsultationType.Id;
            model.Title = adviserConsultationType.Title;
            model.Price = adviserConsultationType.Price;
            model.IsPublished = adviserConsultationType.IsPublished;
            model.AdviserId = adviserConsultationType.AdviserId;

            return model;
        }

        public static ClientSideAdviserConsultationTypeViewModel MapFrom(this ClientSideAdviserConsultationTypeViewModel model, AdviserConsultationType adviserConsultationType)
        {
            model.Id = adviserConsultationType.Id;
            model.Title = adviserConsultationType.Title;
            model.Price = adviserConsultationType.Price;
            model.IsPublished = adviserConsultationType.IsPublished;
            model.AdviserId = adviserConsultationType.AdviserId;

            return model;
        }

        public static AdminSideAdviserViewModel MapFrom(this AdminSideAdviserViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.AdviserUserName = userInfo.UserName;

            return model;
        }
        public static ClientSideAdviserViewModel MapFrom(this ClientSideAdviserViewModel model, ClientSideUserNameAndUserProfileViewModel userInfo)
        {
            model.UserId = userInfo.Id;
            model.AdviserUserName = userInfo.UserName;
            model.AdviserProfile = userInfo.UserAvatar;

            return model;
        }
        public static ClientSideAdviserViewModel MapFrom(this ClientSideAdviserViewModel model, ClientSideBioForMasterModel userInfo)
        {
            model.AdviserBio = userInfo.Bio ?? "";

            return model;
        }
    

        public static AdminSideUpsertAdviserViewModel MapFrom(this AdminSideUpsertAdviserViewModel model, Adviser adviser)
        {
            model.Id = adviser.Id;
            model.Priority = adviser.Priority;
            model.ConsultationPercentage = adviser.ConsultationPercentage;
            model.UserId = adviser.UserId;
            model.IsPublished = adviser.IsPublished;
            model.AdviserConsultationTypes = adviser.ConsultationTypes.Where(x => !x.IsDeleted).Select(type => new AdminSideAdviserConsultationTypeViewModel().MapFrom(type)).ToList() ?? new List<AdminSideAdviserConsultationTypeViewModel>();
            model.AdviserAvailableDates = adviser.AvailableDates.Where(x => !x.IsDeleted).Select(availableDates => new AdminSideAdviserAvailableDateViewModel().MapFrom(availableDates)).ToList() ?? new List<AdminSideAdviserAvailableDateViewModel>();

            return model;
        }
        public static ClientSideAdviserViewModel MapFrom(this ClientSideAdviserViewModel model, Adviser adviser)
        {
            model.Id = adviser.Id;
            model.Priority = adviser.Priority;
            model.ConsultationPercentage = adviser.ConsultationPercentage;
            model.UserId = adviser.UserId;
            model.IsPublished = adviser.IsPublished;
            model.AdviserConsultationTypes = adviser.ConsultationTypes.Where(x => !x.IsDeleted).Select(type => new ClientSideAdviserConsultationTypeViewModel().MapFrom(type)).ToList() ?? new List<ClientSideAdviserConsultationTypeViewModel>();
            model.AdviserAvailableDates = adviser.AvailableDates.Where(x => !x.IsDeleted).Select(availableDates => new ClientSideAdviserAvailableDateViewModel().MapFrom(availableDates)).ToList() ?? new List<ClientSideAdviserAvailableDateViewModel>();

            return model;
        }


        public static AdminSideAdviserViewModel MapFrom(this AdminSideAdviserViewModel model, Adviser adviser)
        {
            model.Id = adviser.Id;
            model.Priority = adviser.Priority;
            model.ConsultationPercentage = adviser.ConsultationPercentage;
            model.UserId = adviser.UserId;
            model.IsPublished = adviser.IsPublished;
            model.AdviserConsultationTypes = adviser.ConsultationTypes.Where(x => !x.IsDeleted).Select(type => new AdminSideAdviserConsultationTypeViewModel().MapFrom(type)).ToList() ?? new List<AdminSideAdviserConsultationTypeViewModel>();
            model.AdviserAvailableDates = adviser.AvailableDates.Where(x => !x.IsDeleted).Select(availableDates => new AdminSideAdviserAvailableDateViewModel().MapFrom(availableDates)).ToList() ?? new List<AdminSideAdviserAvailableDateViewModel>();

            return model;
        }

     

    }
}
