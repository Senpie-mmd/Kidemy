using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.InPersonActiveCourse
{
    public enum InPersonActiveCourseType
    {
        [Display(Name = "Public")]
        Public,

        [Display(Name = "Private")]
        Private,

        [Display(Name = "Seminar")]
        Seminar,

        [Display(Name = "Webinar")]
        Webinar
    }
}
