using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Consultation;
using Kidemy.Domain.Models.Consultation;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Repositories.Consultation
{
    public class AdviserConsultationTypeRespositry : EfRepository<AdviserConsultationType, int>, IAdviserConsultationTypeRespositry
    {
        public AdviserConsultationTypeRespositry(DbContext context) : base(context)
        {
        }
    }
}
