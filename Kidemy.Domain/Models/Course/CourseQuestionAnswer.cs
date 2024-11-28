using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Course
{
    public class CourseQuestionAnswer : AuditBaseEntity<int>
    {
        #region Properties

        public int QuestionId { get; set; }

        public int AnsweredById { get; set; }

        public string Answer { get; set; }

        public bool IsConfirmed { get; set; }

        #endregion

        #region relations
        public CourseQuestion Question { get; set; }
        #endregion

    }

}
