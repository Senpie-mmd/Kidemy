using Kidemy.Application.ViewModels.SocialMedia;
using Kidemy.Domain.Models.SocialMedia;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class SocialMediaMapper
    {
        public static AdminSideUpsertSocialMediaViewModel MapFrom(this AdminSideUpsertSocialMediaViewModel model, SocialMedia socialMedia)
        {
            model.Id = socialMedia.Id;
            model.Link = socialMedia.Link;
            model.Title = socialMedia.Title;
            model.Priority = socialMedia.Priority;
            model.ImageName = socialMedia.ImageName;

            return model;
        }

        public static Expression<Func<SocialMedia, AdminSideSocialMediaViewModel>> MapSocialMediaViewModel => (socialMedia) =>
            new AdminSideSocialMediaViewModel()
            {
                Id = socialMedia.Id,
                Link = socialMedia.Link,
                Title = socialMedia.Title,
                Priority = socialMedia.Priority,
                ImageName = socialMedia.ImageName
            };

        public static AdminSideSocialMediaViewModel MapFrom(this AdminSideSocialMediaViewModel model, SocialMedia socialMedia)
        {
            model.Id = socialMedia.Id;
            model.Title = socialMedia.Title;
            model.Priority = socialMedia.Priority;
            model.ImageName = socialMedia.ImageName;

            return model;
        }

        public static ClientSideSocialMediaViewModel MapFrom(this ClientSideSocialMediaViewModel model, SocialMedia socialMedia)
        {
            model.Id = socialMedia.Id;
            model.Title = socialMedia.Title;
            model.Priority = socialMedia.Priority;
            model.ImageName = socialMedia.ImageName;

            return model;
        }
    }
}