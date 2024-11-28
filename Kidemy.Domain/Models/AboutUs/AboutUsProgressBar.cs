using Barnamenevisan.Localizing.Shared;

namespace Kidemy.Domain.Models.AboutUs
{
    public class AboutUsProgressBar : BaseEntity<int>
    {
        public string Title { get; set; }
        public int Percent { get; set; }
    }
}
