using System.ComponentModel.DataAnnotations;

namespace Kidemy.Domain.Enums.Link
{
    public enum LinkType
    {
        [Display(Name = "Header")]
        Header = 1,
        [Display(Name = "Footer")]
        Footer = 2
    }
}
