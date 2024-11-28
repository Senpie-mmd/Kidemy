using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Course.ClientSideCourse.Courses
{
    public enum FilterCoursesListEnum
    {
        [Display(Name = "MostViewed")]
        MostViewed = 0,
        [Display(Name = "LastUpdated")]
        LastUpdated = 1,
        [Display(Name = "BestSelling")]
        BestSelling = 2,
        [Display(Name = "Newest")]
        Newest = 3,
    }
}
