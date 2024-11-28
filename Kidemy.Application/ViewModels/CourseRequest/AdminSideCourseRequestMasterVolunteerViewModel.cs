using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class AdminSideCourseRequestMasterVolunteerViewModel : BaseEntityViewModel<int>
    {
        public int MasterId { get; set; }

        public string MasterDescription { get; set; }

        public string Mobile { get; set; }
        public DateTime CreatedDateOnUtc { get; set; }

        public string MasterName { get; set; }

    }
}
