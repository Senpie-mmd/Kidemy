using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.VIPMembers
{
    public class VIPPlanConfig : IEntityTypeConfiguration<Domain.Models.VIPMembers.VIPPlan>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.VIPMembers.VIPPlan> builder)
        {
            builder.HasKey(v => v.Id);
            builder.HasQueryFilter(v => !v.IsDeleted);


        }
    }
}
