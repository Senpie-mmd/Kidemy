using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SocialMedia
{
    public class AdminSideSocialMediaViewModel : BaseEntityViewModel<int>
    {
        #region Properties

        [Display(Name = "Priority")]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.OutOfRangeError)]
        public int Priority { get; set; }

        [Display(Name = "Title")]
        [Translate(EntityName = nameof(SocialMedia))]
        public string Title { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        public string ImageName { get; set; }

        #endregion
    }
}