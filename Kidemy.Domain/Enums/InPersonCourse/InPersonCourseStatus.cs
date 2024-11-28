using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.InPersonCourse
{
    public enum InPersonCourseStatus
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Confirmed")]
        Confirmed,

        [Display(Name = "Rejected")]
        Rejected
    }
}
