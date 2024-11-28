using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.CourseRequest
{
    public class CourseRequest : AuditBaseEntity<int>
    {
        public int? PreferedMasterId { get; set; }

        public int RequestedById { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<CourseRequestVote> Votes { get; set; }
        
        public ICollection<CourseRequestMasterVolunteer> MasterVolunteers { get; set; }
        
        public string SelectedTags { get; set; }

        public bool IsConfirm { get; set; }
    }
}
