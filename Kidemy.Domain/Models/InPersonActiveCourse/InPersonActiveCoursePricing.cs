using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.InPersonActiveCourse;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCoursePricing : AuditBaseEntity<int>
    {
        public int ActiveCourseId { get; set; }

        public InPersonActiveCoursePricingType PricingType { get; set; }

        public InPersonActiveCourseType ActiveCourseType { get; set; }

        public int BasePrice { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        public int Capacity { get; set; }

        public bool IsActive { get; set; }

        public InPersonActiveCourse ActiveCourse { get; set; }
    }
}
