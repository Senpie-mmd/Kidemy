using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class ClientSideCourseRequestViewModel : BaseEntityViewModel<int>
    {
        public int RequestedById { get; set; }
        public string Title { get; set; }
        public int? PreferedMasterId { get; set; }
        public string SelectedTags { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateOnUTC { get; set; }
        public int VoteCount { get; set; }
        public bool IsPublished { get; set; }
        
    }
}
