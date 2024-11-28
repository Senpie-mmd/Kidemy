using Kidemy.Application.ViewModels.Link;
using Kidemy.Domain.Models.Link;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class LinkMapper
    {
        public static Expression<Func<Link, AdminSideLinkViewModel>> MapLinkViewModel => (Link link) =>
           new AdminSideLinkViewModel()
           {
               Id = link.Id,
               Title = link.Title,
               Address = link.Address,
               LinkType = link.LinkType
           };

        public static AdminSideUpsertLinkViewModel MapFrom(this AdminSideUpsertLinkViewModel model, Link link)
        {
            model.Id = link.Id;
            model.Title = link.Title;
            model.Address = link.Address;
            model.LinkType = link.LinkType;

            return model;
        }

        public static ClientSideLinkViewModel MapFrom(this ClientSideLinkViewModel model, Link link)
        {
            model.Id = link.Id;
            model.Title = link.Title;
            model.Address = link.Address;
            model.LinkType = link.LinkType;

            return model;
        }
    }
}
