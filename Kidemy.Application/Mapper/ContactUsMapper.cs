using Kidemy.Application.ViewModels.ContactUs;
using Kidemy.Application.ViewModels.Role;
using Kidemy.Domain.Models.ContactUs;
using Kidemy.Domain.Models.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class ContactUsMapper
    {
        public static Expression<Func<ContactUsForm, AdminSideContactUsFormViewModel>> MapContactUsFormViewModel => (ContactUsForm form) =>
            new AdminSideContactUsFormViewModel
            {
                Id = form.Id,
                AnswererId = form.AnswererId,
                CreateDate = form.CreateDate,
                Description = form.Description,
                Email = form.Email,
                FullName = form.FullName,
                ParentId = form.ParentId,
                State = form.State,
                UserId = form.UserId,
            };

        public static AdminSideUpsertContactUsViewModel MapFrom(this AdminSideUpsertContactUsViewModel model, ContactUs contactUs)
        {
            model.Id = contactUs.Id;
            model.Address = contactUs.Address;
            model.Email = contactUs.Email;
            model.Mobile = contactUs.Mobile;
            return model;
        }

        public static AdminSideContactUsFormViewModel MapFrom(this AdminSideContactUsFormViewModel model, ContactUsForm contactUsForm)
        {
            model.Id = contactUsForm.Id;
            model.Description = contactUsForm.Description;
            model.Email = contactUsForm.Email;
            model.CreateDate = contactUsForm.CreateDate;
            model.State = contactUsForm.State;
            model.AnswererId = contactUsForm.AnswererId;
            model.ParentId = contactUsForm.ParentId;
            model.FullName = contactUsForm.FullName;
            model.UserId= contactUsForm.UserId; 
            return model;
        }
    }
}
