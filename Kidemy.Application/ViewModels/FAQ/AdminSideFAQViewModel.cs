using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Application.ViewModels.FAQ
{
    public class AdminSideFAQViewModel : BaseEntityViewModel<int>
    {
        [Translate(EntityName = nameof(FAQ))]
        public string Title { get; set; }

        [Translate(EntityName = nameof(FAQ))]
        public string Answer { get; set; }

    }
}
