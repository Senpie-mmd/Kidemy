using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.InPersonActiveCourse
{
    public enum InPersonActiveCourseState
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Performing")]
        Performing,

        [Display(Name = "Registering")]
        Registering,

        [Display(Name = "InProgress")]
        InProgress,

        [Display(Name = "Ended")]
        Ended,

        [Display(Name = "Canceled")]
        Canceled,

        [Display(Name = "Rejected")]
        Rejected
    }
}
