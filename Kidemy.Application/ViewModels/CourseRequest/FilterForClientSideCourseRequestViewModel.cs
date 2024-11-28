using Barnamenevisan.Localizing.Shared;
using Kidemy.Application.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.CourseRequest
{
    public class FilterForClientSideCourseRequestViewModel : BasePaging<ClientSideCourseRequestViewModel>
    {
        public string? Title { get; set; }
    }
}
