using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.Branch
{
    public class Branch : AuditBaseEntity<int>
    {
        public int OwnerId { get; set; }

        public int? ParentId { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string? Fax { get; set; }

        public string? GoogleMap { get; set; }

        public string? GoogleMapIFrameAddress { get; set; }

        public string? Description { get; set; }

        public string? CarTransportPath { get; set; }

        public string? PublicTransportPath { get; set; }

        public int Percentage { get; set; }

        public string? Logo { get; set; }

        public string? HeaderBanner { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public bool ShowInBranchesList { get; set; }

        public bool PayamNourPhdBranch { get; set; }


        public Branch? Parent { get; set; }

        public ICollection<Branch> Children { get; set; }

        public ICollection<BranchManager> Managers { get; set; }

        public ICollection<BranchClass> Classes { get; set; }
    }
}
