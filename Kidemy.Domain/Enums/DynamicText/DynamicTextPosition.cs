using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.DynamicText
{
    public enum DynamicTextPosition
    {
        [Display(Name = "VIPPageText")]
        VIPPageText = 1,
        [Display(Name = "VIPInvoiceText")]
        VIPInvoiceText = 2,
        [Display(Name = "VIPDashBoardTextBeforeJoin")]
        VIPDashBoardTextBeforeJoin = 3,
        [Display(Name = "VIPDashBoardTextAfterJoin")]
        VIPDashBoardTextAfterJoin = 4,
        [Display(Name = "TitleOfTopOfHomePage")]
        TitleOfTopOfHomePage = 5,
        [Display(Name = "DescriptionOfTopOfHomePage")]
        DescriptionOfTopOfHomePage = 6,
        [Display(Name = "TheFirstFeature")]
        TheFirstFeature = 7,
        [Display(Name = "TheSecondFeature")]
        TheSecondFeature = 8,
        [Display(Name = "TheThirdFeature")]
        TheThirdFeature = 9,
        [Display(Name = "TextOfTheMostPopularCoursesOfHomePage")]
        TextOfTheMostPopularCoursesOfHomePage = 10,
        [Display(Name = "TextOfTheProposedCoursesOfHomePage")]
        TextOfTheProposedCoursesOfHomePage = 11,
        [Display(Name = "UserCount")]
        UserCount = 12,
        [Display(Name = "FirstPageBannerText")]
        FirstPageBannerText = 13,
    }
}
