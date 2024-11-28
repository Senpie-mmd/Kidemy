using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Enums.ContactUs
{
    public enum ContactUsFormState
    {
        [Display(Name = "All")]
        All,

        [Display(Name = "Answered")]
        Answered,

        [Display(Name = "NotAnswered")]
        NotAnswered,

        Response
    }

    //WithOut response
    public enum ContactUsStateForFilter
    {
        [Display(Name = "All")]
        All,

        [Display(Name = "Answered")]
        Answered,

        [Display(Name = "NotAnswered")]
        NotAnswered,
    }
}
