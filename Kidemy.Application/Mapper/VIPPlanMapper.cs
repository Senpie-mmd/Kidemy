using Kidemy.Application.ViewModels.Link;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Application.ViewModels.VIPPlan;
using Kidemy.Domain.Models.Link;
using Kidemy.Domain.Models.Page;
using Kidemy.Domain.Models.VIPMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class VIPPlanMapper
    {
        public static ClientSideVIPPlanViewModel MapFrom(this ClientSideVIPPlanViewModel model, VIPPlan vIPPlan)
        {
            model.Id = vIPPlan.Id;
            model.Price = vIPPlan.Price;
            model.Title = vIPPlan.Title;
            model.DurationDay = vIPPlan.DurationDay;

            return model;
        }

        public static AdminSideVIPPlanViewModel MapFrom(this AdminSideVIPPlanViewModel model, VIPPlan vIPPlan)
        {
            model.Id = vIPPlan.Id;
            model.Price = vIPPlan.Price;
            model.Title = vIPPlan.Title;
            model.DurationDay = vIPPlan.DurationDay;

            return model;
        }
        public static Expression<Func<VIPPlan, AdminSideVIPPlanViewModel>> MapVIPPlanViewModel => (VIPPlan vIPPlan) =>
          new AdminSideVIPPlanViewModel()
          {
              Id = vIPPlan.Id,
              Title = vIPPlan.Title,
              DurationDay = vIPPlan.DurationDay,
              Price = vIPPlan.Price,
              IsPublished = vIPPlan.IsPublished,
          };

        public static AdminSideUpsertVIPPlanViewModel MapFrom(this AdminSideUpsertVIPPlanViewModel model, VIPPlan page)
        {
            model.Id = page.Id;
            model.Title = page.Title;
            model.Price= page.Price;
            model.DurationDay = page.DurationDay;
            model.IsPublished = page.IsPublished;

            return model;
        }
    }
}
