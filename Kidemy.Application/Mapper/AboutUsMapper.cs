using Kidemy.Application.ViewModels.AboutUs;
using Kidemy.Domain.Models.AboutUs;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class AboutUsMapper
    {
        public static Expression<Func<AboutUsProgressBar, ProgressBarListViewModel>> MapAdminSideTicketViewModel => (AboutUsProgressBar progressBar) => new ProgressBarListViewModel
        {
            Title = progressBar.Title,
            Id = progressBar.Id,
            Percent = progressBar.Percent
        };

        public static AdminSideUpsertAboutUsViewModel MapFrom(this AdminSideUpsertAboutUsViewModel model, AboutUs aboutUs)
        {
            model.Id = aboutUs.Id;
            model.Title = aboutUs.Title;
            model.Description = aboutUs.Description;
            model.OurGoal = aboutUs.OurGoal;
            model.OurGoalTitle = aboutUs.OurGoalTitle;
            model.OurGoalDescription = aboutUs.OurGoalDescription;
            model.PageTitle = aboutUs.PageTitle;
            model.OurGoalFeatures = aboutUs.OurGoalFeatures;
            model.ImageNumber1 = aboutUs.ImageNumber1;
            model.ImageNumber2 = aboutUs.ImageNumber2;
            model.ImageNumber3 = aboutUs.ImageNumber3;
            model.ImageNumber4 = aboutUs.ImageNumber4;
            model.ImageNumber5 = aboutUs.ImageNumber5;

            return model;
        }

        public static ProgressBarListViewModel MapFrom(this ProgressBarListViewModel model, AboutUsProgressBar progressBar)
        {
            model.Id = progressBar.Id;
            model.Title = progressBar.Title;
            model.Percent = progressBar.Percent;

            return model;
        }

        public static UpdateAboutUsProgressBarViewModel MapFrom(this UpdateAboutUsProgressBarViewModel model, AboutUsProgressBar progressBar)
        {
            model.Title = progressBar.Title;
            model.Percent = progressBar.Percent;

            return model;
        }

        public static ClientSideAboutUsPageViewModel MapFrom(this ClientSideAboutUsPageViewModel model, AboutUs aboutUs)
        {
            model.Id = aboutUs.Id;
            model.Title = aboutUs.Title;
            model.Description = aboutUs.Description;
            model.OurGoal = aboutUs.OurGoal;
            model.OurGoalTitle = aboutUs.OurGoalTitle;
            model.OurGoalDescription = aboutUs.OurGoalDescription;
            model.PageTitle = aboutUs.PageTitle;
            model.OurGoalFeatures = aboutUs.OurGoalFeatures;
            model.ImageNumber1 = aboutUs.ImageNumber1;
            model.ImageNumber2 = aboutUs.ImageNumber2;
            model.ImageNumber3 = aboutUs.ImageNumber3;
            model.ImageNumber4 = aboutUs.ImageNumber4;
            model.ImageNumber5 = aboutUs.ImageNumber5;

            return model;
        }
    }
}
