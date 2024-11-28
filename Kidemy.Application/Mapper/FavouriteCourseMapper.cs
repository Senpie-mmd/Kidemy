using Kidemy.Application.ViewModels.Consultation.Adviser;
using Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses;
using Kidemy.Application.ViewModels.FavouriteCourse;
using Kidemy.Domain.Models.Consultation;
using Kidemy.Domain.Models.Course;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class FavouriteCourseMapper
    {
        public static Expression<Func<FavouriteCourse, ClientSideFavouriteCourseViewModel>> MapClientSideFavouriteCourseViewModel => (FavouriteCourse favouriteCourse) =>
  new ClientSideFavouriteCourseViewModel()
  {
      UserId = favouriteCourse.UserId,
      CourseId = favouriteCourse.CourseId,
  };

        public static ClientSideFavouriteCourseViewModel MapFrom(this ClientSideFavouriteCourseViewModel model, ClientSideGetSlugsViewModel course)
        {
            model.Slug = course.Slugs;
            model.CourseTitle = course.Title;
            return model;
        }
    }
}
