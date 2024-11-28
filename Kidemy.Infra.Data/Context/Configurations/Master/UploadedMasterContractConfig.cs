using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Master
{
    public class UploadedMasterContractConfig : IEntityTypeConfiguration<Domain.Models.Master.UploadedMasterContract>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Master.UploadedMasterContract> builder)
        {
            builder.HasKey(mc => mc.Id);

            builder.Property(mc => mc.FileName).HasMaxLength(60);

            builder.HasQueryFilter(mc => !mc.IsDeleted);

            builder.HasOne(n => n.Master)
                .WithMany(n => n.UploadedMasterContracts)
                .HasForeignKey(n => n.MasterId);

            builder.HasOne(n => n.MasterContract)
                .WithMany(n => n.UploadedMasterContracts)
                .HasForeignKey(n => n.MasterContractId);
        }
    }
}
