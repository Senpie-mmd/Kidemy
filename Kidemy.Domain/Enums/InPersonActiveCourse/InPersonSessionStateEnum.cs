using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.InPersonActiveCourse
{
    public enum InPersonSessionStateEnum
    {
        [Display(Name = "Pending")]
        Pending,

        [Display(Name = "Accepted")]
        Accepted,

        [Display(Name = "Rejected")]
        Rejected
    }
}
