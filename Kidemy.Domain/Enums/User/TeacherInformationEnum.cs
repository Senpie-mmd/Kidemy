using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Domain.Enums.User
{
    public class TeacherInformationEnum
    {
        public enum TeacherInformationStatus
        {
            [Display(Name = "Pending")]
            Pending,

            [Display(Name = "Confirmed")]
            Confirmed,

            [Display(Name ="Reject")]
            Reject,
        }

    }
}
