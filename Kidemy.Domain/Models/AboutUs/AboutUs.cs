using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.AboutUs
{
    public class AboutUs : BaseEntity<int>
    {
        public string? PageTitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? OurGoal { get; set; }
        public string? OurGoalTitle { get; set; }
        public string? OurGoalDescription { get; set; }
        public string? OurGoalFeatures { get; set; }
        public string? ImageNumber1 { get; set; }
        public string? ImageNumber2 { get; set; }
        public string? ImageNumber3 { get; set; }
        public string? ImageNumber4 { get; set; }
        public string? ImageNumber5 { get; set; }
    }
}
