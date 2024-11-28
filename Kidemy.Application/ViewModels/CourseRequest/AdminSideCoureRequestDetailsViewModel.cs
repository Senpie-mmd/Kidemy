using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class AdminSideCoureRequestDetailsViewModel : BaseEntityViewModel<int>
    {
        public string Title { get; set; }
        public int VoteCount { get; set; }
        public string SelectedTags { get; set; }

        public string? PreferedMasterName { get; set; }
        public DateTime CreatedDateOnUTC { get; set; }
        public string Description { get; set; }     
        public List<AdminSideCourseRequestMasterVolunteerViewModel> MasterVolunteersInfo { get; set; }

    }
}
