using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SocialMedia
{
    public class AdminSideUpsertSocialMediaViewModel : LocalizableViewModel<LocalizedAdminSideUpsertSocialMediaViewModel>
    {
        #region Constructor

        public AdminSideUpsertSocialMediaViewModel()
        {
            ImageName = SiteTools.DefaultImageName;
        }

        #endregion

        #region Properties

        [Display(Name = "Priority")]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Link { get; set; }

        public string ImageName { get; set; }

        public IFormFile? Image { get; set; }

        #endregion
    }

    public class LocalizedAdminSideUpsertSocialMediaViewModel : LocalizedViewModel
    {
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Title { get; set; }
    }
}