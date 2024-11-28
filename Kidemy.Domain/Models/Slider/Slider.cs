using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Slider
{
    public class Slider : AuditBaseEntity<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ImageName { get; set; }
        public int Priority { get; set; }
        public SliderPlace SliderPlace { get; set; }
        public string? URL { get; set; }
        public bool IsPublished { get; set; }
    }
}
