using Barnamenevisan.Localizing.Attributes;
using Barnamenevisan.Localizing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kidemy.Application.ViewModels.MasterContract
{
    public class AdminSideMasterContractViewModel : BaseEntityViewModel<int>
    {
        [Display(Name = "Title")]
        [Translate(EntityName = nameof(Domain.Models.Master.MasterContract))]
        public string Title { get; set; }

        [Display(Name = "FileName")]
        public string FileName { get; set; }

        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }
    }
}
