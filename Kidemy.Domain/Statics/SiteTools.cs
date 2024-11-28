using System.Globalization;

namespace Kidemy.Domain.Statics;

public class SiteTools
{
    #region Default

    public static string DefaultImageName { get; set; }

    #endregion

    #region Course

    public static string CourseImagesPath { get; set; }

    public static string CourseVideosPath { get; set; }

    public static string CourseDetailsFiles { get; set; }

    #endregion

    #region SocialMedia

    public static string SocialMediaImagePath { get; set; }

    public static string SocialMediaImageThumbPath { get; set; }


    #endregion

    #region Site setting

    public static string SiteName { get; set; }

    public static string SiteAddress { get; set; }

    #endregion

    #region User

    public static string UserImagePath { get; set; }

    public static string UserImageThumbPath { get; set; }


    #endregion

    #region ZarinPal

    public static string ZarinPalPaymentAddress { get; set; }

    public static string ZarinPalMerchantId { get; set; }

    public static bool IsZarinPalSandBox { get; set; }

    #endregion

    #region Sms

    public static int SmsCodeLength { get; set; }

    public static int ExpireSmsCodeInMinutes { get; set; }

    #endregion

    #region Mail

    public static int MailCodeLength { get; set; }

    public static int ExpireMailCodeInMinutes { get; set; }

    #endregion

    #region Culture

    public static string DefaultCultureName { get; set; }
    public static List<CultureInfo> SupportedCultures { get; set; }


    #endregion

    #region Article

    public static string ArticleImagePath { get; set; }

    public static string ArticleImageThumbPath { get; set; }


    #endregion

    #region AboutUs

    public static string AboutUsImagePath { get; set; }


    #endregion

    #region Cache

    public const int CachesDefaultExpirationTimeInMinutes = 2;

    #endregion

    #region SecretKey

    public static string SecretKey => "Avmi3rAvmhMM@face2wadc";

    #endregion

    #region SiteSetting

    public static string SiteSettingImagePath { get; set; }

    public static string SiteSettingImageThumbPath { get; set; }


    #endregion

    #region Master

    public static string MasterImagePath { get; set; }

    public static string MasterImageThumbPath { get; set; }


    #endregion

    #region MasterContract

    public static string MasterContractFilePath { get; set; }


    #endregion

    #region UploadedMasterContract

    public static string UploadedMasterContractFilePath { get; set; }


    #endregion

    #region CourseCategory
    public static string CourseCategoryImagePath { get; set; }

    public static string CourseCategoryImageThumbPath { get; set; }
    #endregion

    #region CourseQuestion
    public static string CourseQuestionImagePath { get; set; }

    public static string CourseQuestionImageThumbPath { get; set; }
    #endregion

    #region Slider

    public static string SliderImagePath { get; set; }

    public static string SliderImageThumbPath { get; set; }


    #endregion

    #region Banner

    public static string BannerImagePath { get; set; }

    public static string BannerImageThumbPath { get; set; }


    #endregion

    #region MasterNotification
    public static string MasterNotificationVideosPath { get; set; }
    #endregion

    #region Blog

    public static string BlogImagePath { get; set; }
    public static string BlogImageThumPath { get; set; }

    #endregion

    #region InPersonCourse

    public static string InPersonCourseImagesPath { get; set; }

    #endregion
}
