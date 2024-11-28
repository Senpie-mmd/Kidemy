using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.InPersonActiveCourse;

namespace Kidemy.Domain.Models.InPersonActiveCourse
{
    public class InPersonActiveCourse : AuditBaseEntity<int>
    {
        public int? ClassId { get; set; }

        public int BranchId { get; set; }

        public int CourseId { get; set; }

        public int MasterId { get; set; }

        public bool IsMasterWalletSettled { get; set; }

        public InPersonActiveCourseState ActiveCourseState { get; set; }

        public string? AttachmentFileName { get; set; }

        public string? MasterVideoName { get; set; }

        public string? MasterVideoNameCover { get; set; }

        public string? Description { get; set; }

        public DateTime StartRegisterationDateTimeOnUtc { get; set; }

        public DateTime EndRegisterationDateTimeOnUtc { get; set; }

        public DateTime StartDateOnUtc { get; set; }

        public DateTime? EndDateOnUtc { get; set; }

        public int CourseTotalHours { get; set; }

        public int MasterCommissionPercentage { get; set; }

        public int BranchCommissionPercentage { get; set; }

        public string? OnlineClassLink { get; set; }

        public bool IsActive { get; set; }

        public bool ShowStartDate { get; set; }

        public bool ShowInHomePage { get; set; }

        public bool ShowActiveCourseInHoldCourses { get; set; }

        public bool HideMastersShare { get; set; }

        public ICollection<InPersonActiveCourseTiming> Timings { get; set; }

        public ICollection<InPersonActiveCoursePricing> Pricings { get; set; }

        public ICollection<InPersonActiveCourseSession> Sessions { get; set; }

        public ICollection<InPersonActiveCourseVideo> Videos { get; set; }

        public ICollection<InPersonActiveCourseImage> Images { get; set; }
    }
}
