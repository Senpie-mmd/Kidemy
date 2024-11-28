using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using Kidemy.Domain.Enums.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.Link
{
    public class AdminSideLinkViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(Link))]
        public string Title { get; set; }
        public string Address { get; set; }
        public LinkType LinkType { get; set; }
    }
}
