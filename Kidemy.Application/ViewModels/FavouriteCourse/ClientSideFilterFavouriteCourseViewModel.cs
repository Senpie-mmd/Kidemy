using Barnamenevisan.Localizing.Shared;
using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.FavouriteCourse
{
    public class ClientSideFilterFavouriteCourseViewModel : BasePaging<ClientSideFavouriteCourseViewModel>
    {
        public int UserId { get; set; }
    }
}
