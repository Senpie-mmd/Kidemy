using Barnamenevisan.Localizing.ViewModels;

namespace Kidemy.Application.ViewModels.Newsletter
{
    public class AdminSideNewsletterViewModel : BaseEntityViewModel<int>
    {
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserIp { get; set; }
    }
}
