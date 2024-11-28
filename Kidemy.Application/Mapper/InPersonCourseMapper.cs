using Kidemy.Application.ViewModels.InPersonCourse;
using Kidemy.Application.ViewModels.Page;
using Kidemy.Domain.Models.InPersonCourse;
using Kidemy.Domain.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.Mapper
{
    public static class InPersonCourseMapper
    {
        public static Expression<Func<InPersonCourse, ClientSideInPersonCourseViewModel>> MapInPersonCourseViewModel => (InPersonCourse inPersonCourse) =>
        new ClientSideInPersonCourseViewModel()
        {
            AttachmentFileName = inPersonCourse.AttachmentFileName,
            Title = inPersonCourse.Title,
            Description = inPersonCourse.Description,
            IsPublished = inPersonCourse.IsPublished,
            CourseTopics = inPersonCourse.CourseTopics,
            MetaTag = inPersonCourse.MetaTag,
            Priority = inPersonCourse.Priority,
            Status = inPersonCourse.Status,
            ShortDescription = inPersonCourse.ShortDescription,
            CourseGoals = inPersonCourse.CourseGoals,
            ImageName = inPersonCourse.ImageName,
            CertificateAvailable = inPersonCourse.CertificateAvailable,
            CourseAudience = inPersonCourse.CourseAudience,
            CourseRequirements = inPersonCourse.CourseRequirements,
            PrivateClassAvailable = inPersonCourse.PrivateClassAvailable

        };
    }
}
