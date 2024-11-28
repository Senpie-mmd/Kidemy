using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Enums.Course
{
    public enum CourseType
    {
        [Display(Name = "Free")]
        Free,
        [Display(Name = "Mercenary")]
        Mercenary,
        [Display(Name = "VIP")]
        VIP,
    }
}
