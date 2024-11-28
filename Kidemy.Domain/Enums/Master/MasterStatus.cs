using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Master
{
    public enum MasterStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Confirmed")]
        Confirmed,
        [Display(Name = "NonConfirmed")]
        NonConfirmed
    }
}
