using Kidemy.Application.ViewModels.Newsletter;
using Kidemy.Domain.Models.Newsletter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class NewsletterMapper
    {
        public static Expression<Func<Newsletter, AdminSideNewsletterViewModel>> MapAdminSideNewsletterForAdmin => (Newsletter newsletter) => new AdminSideNewsletterViewModel
        {
            Email = newsletter.Email,
            UserIp = newsletter.Ip,
            Id = newsletter.Id,
            CreateDate = newsletter.CreatedDateOnUtc
        };
        public static RegisterNewsletterViewModel MapFrom(this RegisterNewsletterViewModel model, Newsletter newsletter)
        {
            model.Email = newsletter.Email;
            return model;
        }

        public static AdminSideNewsletterViewModel MapFrom(this AdminSideNewsletterViewModel model, Newsletter newsletters)
        {
            model.Email = newsletters.Email;
            model.UserIp = newsletters.Ip;
            model.CreateDate = newsletters.CreatedDateOnUtc;

            return model;
        }
    }
}
