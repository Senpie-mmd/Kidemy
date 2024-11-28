using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Sms;
using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.SiteSetting
{
    public class AdminSideUpsertSiteSettingViewModel : LocalizableViewModel<LocalizedAdminSideUpsertSiteSettingViewModel>
    {
        public AdminSideUpsertSiteSettingViewModel()
        {
            LogoName = SiteTools.DefaultImageName;
        }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
        [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]*$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string? Mobile { get; set; }

        [Display(Name = "Mobile2")]
        [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
        [RegularExpression("^[0-9]*$", ErrorMessage = ErrorMessages.MobileFormatError)]
        public string? Mobile2 { get; set; }

        [Display(Name = "Address")]
        [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
        public string? Address { get; set; }

        [Display(Name = "Logo")]
        [MaxLength(50)]
        public string? LogoName { get; set; }

        public IFormFile? LogoImage { get; set; }  

        [Display(Name = "CollectionManagement")]
        public string? CollectionManagement { get; set; }

        //[Display(Name = "FooterDescription")]
        //public string? FooterDescription { get; set; }

        [Display(Name = "CopyrightDescription")]
        public string? CopyrightDescription { get; set; }

        [Display(Name = "SmsProviderType")]
        public SmsProviderType SmsProviderType { get; set; }

        [Display(Name = "NewsletterDescription")]
        public string? NewsletterDescription { get; set; }
        [Display(Name = "WithdrawRequestMinimumAmount")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        [DataType(DataType.Currency,ErrorMessage =ErrorMessages.FormatError)]
        public decimal? WithdrawRequestMinimumAmount { get; set; }

    }
    public class LocalizedAdminSideUpsertSiteSettingViewModel : LocalizedViewModel
    {
        [Display(Name = "CollectionManagement")]
        public string? CollectionManagement { get; set; }

        [Display(Name = "Address")]
        [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
        [UIHint("TextArea")]
        public string? Address { get; set; }       

        //[Display(Name = "FooterDescription")]
        //[UIHint("ckeditor")]
        //public string? FooterDescription { get; set; }

        [Display(Name = "CopyrightDescription")]
        [UIHint("ckeditor")]
        public string? CopyrightDescription { get; set; }

        [Display(Name = "NewsletterDescription")]
        [UIHint("ckeditor")]
        public string? NewsletterDescription { get; set; }

        

    }
}
