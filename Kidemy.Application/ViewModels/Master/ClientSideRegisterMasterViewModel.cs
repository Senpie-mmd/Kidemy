using Kidemy.Domain.Shared;
using Kidemy.Domain.Statics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kidemy.Application.ViewModels.Master
{
    public class ClientSideRegisterMasterViewModel
    {
        public ClientSideRegisterMasterViewModel()
        {
            IdentificationFileName = SiteTools.DefaultImageName;
        }

        public int Id { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Bio { get; set; }

        [Display(Name = "FatherName")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string FatherName { get; set; }

        [Display(Name = "NationalIDNumber")]
        [MaxLength(10, ErrorMessage = ErrorMessages.MaxLengthError)]
        [MinLength(10, ErrorMessage = ErrorMessages.MinLengthErrorForNationalID)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = ErrorMessages.PleaseEnterOnlyNumbersForNationalID)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string NationalIDNumber { get; set; }

        [Display(Name = "IdentificationNumber")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string IdentificationNumber { get; set; }

        [Display(Name = "PlaceOfIssuance")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string PlaceOfIssuance { get; set; }

        [Display(Name = "PostalCode")]
        [MaxLength(10, ErrorMessage = ErrorMessages.MaxLengthError)]
        [MinLength(10, ErrorMessage = ErrorMessages.MinLengthErrorForPostalCode)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = ErrorMessages.PleaseEnterOnlyNumbersForPostalCode)]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string PostalCode { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public string Address { get; set; }

        [Display(Name = "IdentificationFile")]
        [Required(ErrorMessage = ErrorMessages.RequiredError)]
        public IFormFile IdentificationFile { get; set; }

        [Display(Name = "IdentificationFileName")]
        public string? IdentificationFileName { get; set; }
    }
}
