using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Context.Configurations.Ticket
{
    public class TicketConfig : IEntityTypeConfiguration<Domain.Models.Ticket.Ticket>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Ticket.Ticket> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(e => e.Title).HasMaxLength(500);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasIndex(t => t.OwnerUserId).IsClustered(false);
        }
    }
}
