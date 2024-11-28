using Barnamenevisan.Localizing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Domain.Interfaces.Newsletter
{
    public interface INewsletterRepository : IRepository<Models.Newsletter.Newsletter, int>
    {
    }
}
