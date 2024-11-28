using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Models.Page;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class PageMapper
    {
        public static Expression<Func<Page, AdminSidePageViewModel>> MapPageViewModel => (Page page) =>
           new AdminSidePageViewModel()
           {
               Id = page.Id,
               Title = page.Title,
               Body = page.Body,
               Slug = page.Slug,
               IsPublished = page.IsPublished,
               LinkPlace = page.LinkPlace
           };

        public static AdminSideUpsertPageViewModel MapFrom(this AdminSideUpsertPageViewModel model, Page page)
        {
            model.Id = page.Id;
            model.Title = page.Title;
            model.Body = page.Body;
            model.Slug = page.Slug;
            model.IsPublished = page.IsPublished;
            model.LinkPlace = page.LinkPlace;

            return model;
        }

        public static ClientSidePageViewModel MapFrom(this ClientSidePageViewModel model, Page page)
        {
            model.Id = page.Id;
            model.Title = page.Title;
            model.Body = page.Body;
            model.Slug = page.Slug;
            model.IsPublished = page.IsPublished;
            model.LinkPlace = page.LinkPlace;

            return model;
        }
    }
}
