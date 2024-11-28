using Kidemy.Application.ViewModels.FAQ;
using Kidemy.Domain.Models.FAQ;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class FAQMapper
    {
        public static Expression<Func<FAQ, AdminSideFAQViewModel>> MapFAQViewModel => (FAQ fAQ) =>
        new AdminSideFAQViewModel()
        {
            Id = fAQ.Id,
            Title = fAQ.Title,
            Answer = fAQ.Answer

        };

        public static AdminSideUpsertFAQViewModel MapFrom(this AdminSideUpsertFAQViewModel model, FAQ fAQ)
        {
            model.Id = fAQ.Id;
            model.Title = fAQ.Title;
            model.Answer = fAQ.Answer;

            return model;
        }

        public static ClientSideFAQViewModel MapFrom(this ClientSideFAQViewModel model, FAQ fAQ)
        {
            model.Id = fAQ.Id;
            model.Title = fAQ.Title;
            model.Answer = fAQ.Answer;

            return model;
        }

    }
}
