using Kidemy.Application.ViewModels.Slider;
using Kidemy.Domain.Models.Slider;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class SliderMapper
    {

        public static AdminSideUpdateSliderViewModel MapFrom(this AdminSideUpdateSliderViewModel model, Slider slider)
        {
            model.Id = slider.Id;
            model.Title = slider.Title;
            model.Description = slider.Description;
            model.IsPublished = slider.IsPublished;
            model.ImageName = slider.ImageName;
            model.Priority = slider.Priority;
            model.SliderPlace = slider.SliderPlace;
            model.URL = slider.URL;

            return model;
        }

        public static ClientSideSliderViewModel MapFrom(this ClientSideSliderViewModel model, Slider slider)
        {
            model.Id = slider.Id;
            model.Title = slider.Title;
            model.Description = slider.Description;
            model.IsPublished = slider.IsPublished;
            model.ImageName = slider.ImageName;
            model.Priority = slider.Priority;
            model.SliderPlace = slider.SliderPlace;
            model.URL = slider.URL;

            return model;
        }

        public static Expression<Func<Slider, AdminSideSliderViewModel>> MapSliderViewModel => (Slider slider) =>
          new AdminSideSliderViewModel
          {
              Id = slider.Id,
              Title = slider.Title,
              Description = slider.Description,
              ImageName = slider.ImageName,
              URL = slider.URL,
              SliderPlace = slider.SliderPlace,
              Priority = slider.Priority,
              IsPublished = slider.IsPublished
          };
    }
}
