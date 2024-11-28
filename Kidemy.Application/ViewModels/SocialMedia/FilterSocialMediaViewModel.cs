using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SocialMedia
{
    public class FilterSocialMediaViewModel : BasePaging<AdminSideSocialMediaViewModel>
    {
        #region Filter Properties

        [Display(Name = "Title")]
        [FilterByLocalizedValue]
        public string? Title { get; set; }

        #endregion
    }
}