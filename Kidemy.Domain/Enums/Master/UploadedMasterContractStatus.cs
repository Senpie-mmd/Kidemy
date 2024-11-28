using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Master
{
    public enum UploadedMasterContractStatus
    {
        [Display(Name = "PendingUpload")]
        PendingUpload,
        [Display(Name = "PendingReview")]
        PendingReview,
        [Display(Name = "IsConfirmed")]
        IsConfirmed,
    }
}
