using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.VIPMembers
{
    public class VIPMembersConfig : IEntityTypeConfiguration<Domain.Models.VIPMembers.VIPMember>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.VIPMembers.VIPMember> builder)
        {
            builder.HasKey(v => v.Id);
            builder.HasQueryFilter(v => !v.IsDeleted);

            builder
                .HasOne(v => v.VIPPlan)
                .WithMany(v => v.VIPMembers)
                .HasForeignKey(v => v.VIPPlanId);

        }
    }
}
