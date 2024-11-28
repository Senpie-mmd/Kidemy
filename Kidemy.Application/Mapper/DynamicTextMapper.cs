using Kidemy.Application.ViewModels.DynamicText;
using Kidemy.Application.ViewModels.Link;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Models.DynamicText;
using Kidemy.Domain.Models.Link;
using Kidemy.Domain.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class DynamicTextMapper
    {
        public static ClientSideDynamicTextViewModel MapFrom(this ClientSideDynamicTextViewModel model, DynamicText dynamicText)
        {
            model.Id = dynamicText.Id;
            model.Text = dynamicText.Text;
            model.Position = dynamicText.Position;

            return model;
        }
        public static Expression<Func<DynamicText, AdminSideDynamicTextViewModel>> MapDynamicTextViewModel => (DynamicText dynamicText) =>
           new AdminSideDynamicTextViewModel()
           {
               Id = dynamicText.Id,
               Text = dynamicText.Text,
               Position = dynamicText.Position

           };

        public static AdminSideUpdateDynamicTextViewModel MapFrom(this AdminSideUpdateDynamicTextViewModel model, DynamicText dynamicText)
        {
            model.Id = dynamicText.Id;
            model.Text = dynamicText.Text;
            model.Position = dynamicText.Position;

            return model;
        }
    }
}
