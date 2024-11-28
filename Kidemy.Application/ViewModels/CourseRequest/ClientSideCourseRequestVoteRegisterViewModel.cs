using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class ClientSideCourseRequestVoteRegisterViewModel
    {
        public int CourseRequestId { get; set; }

        public int UserId { get; set; }

        public bool IsAgree { get; set; }
    }
}
