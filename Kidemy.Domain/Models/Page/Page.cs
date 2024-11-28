using Barnamenevisan.Localizing.Shared;
using Kidemy.Domain.Enums.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Models.Page
{
    public class Page : AuditBaseEntity<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public LinkPlace LinkPlace { get; set; }
    }
}
