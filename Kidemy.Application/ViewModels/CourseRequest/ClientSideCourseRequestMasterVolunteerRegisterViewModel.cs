using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class ClientSideCourseRequestMasterVolunteerRegisterViewModel
    {
        public int CourseRequestId { get; set; }

        public int MasterId { get; set; }

        public string MasterDescription { get; set; }

        public string? AdminDescription { get; set; }

    }
}
