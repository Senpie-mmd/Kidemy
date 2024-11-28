using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.DynamicText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.DynamicText
{
    public class DynamicText : BaseEntity<int>
    {
        public string Text { get; set; }
        public DynamicTextPosition Position { get; set; }
    }
}
