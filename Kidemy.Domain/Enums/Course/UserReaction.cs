using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Enums.Course
{
    public enum UserReaction
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Useful")]
        Useful,
        [Display(Name = "Useless")]
        Useless

    }
}
