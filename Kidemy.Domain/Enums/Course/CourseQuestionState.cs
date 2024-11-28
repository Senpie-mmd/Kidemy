using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Course
{
    public enum CourseQuestionState
    {
        [Display(Name = "All")]
        All,

        [Display(Name = "Confirmed")]
        Confirmed,

        [Display(Name = "NotConfirmed")]
        NotConfirmed,
    }
}
