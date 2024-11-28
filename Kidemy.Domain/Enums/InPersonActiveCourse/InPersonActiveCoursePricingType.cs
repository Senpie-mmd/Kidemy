using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.InPersonActiveCourse
{
    public enum InPersonActiveCoursePricingType
    {
        [Display(Name = "InPerson")]
        InPerson,

        [Display(Name = "Online")]
        Online
    }
}
