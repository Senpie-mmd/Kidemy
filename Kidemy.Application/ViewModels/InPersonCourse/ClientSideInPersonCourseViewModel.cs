using Kidemy.Domain.Enums.InPersonCourse;
using Kidemy.Domain.Models.InPersonCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.InPersonCourse
{
    public class ClientSideInPersonCourseViewModel
    {
        public string Title { get; set; }

        public string Slug { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public string? CourseRequirements { get; set; }

        public string? CourseTopics { get; set; }

        public string? CourseGoals { get; set; }

        public string? CourseAudience { get; set; }

        public string? AttachmentFileName { get; set; }

        public string? MetaTag { get; set; }

        public bool CertificateAvailable { get; set; }

        public bool IsPublished { get; set; }

        public bool PrivateClassAvailable { get; set; }

        public InPersonCourseStatus Status { get; set; }

        public int Priority { get; set; }

    }
}
