namespace Kidemy.Domain.Models.Discount
{
    public class DiscountLimitationCourseMapping
    {
        #region Properties

        public int DiscountLimitationId { get; set; }

        public int CourseId { get; set; }

        #endregion

        #region Relations

        public DiscountLimitation DiscountLimitation { get; set; }

        #endregion
    }
}
