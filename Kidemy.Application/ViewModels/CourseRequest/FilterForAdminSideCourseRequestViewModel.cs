using Barnamenevisan.Localizing.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class FilterForAdminSideCourseRequestViewModel : BasePaging<AdminSideCourseRequestViewModel>
    {
        [Display(Name = "Title")]
        public string? Title { get; set; }

        public bool? IsConfirm { get; set; }
    }
}
