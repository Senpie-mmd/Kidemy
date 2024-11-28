using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Context.Configurations.Ticket
{
    public class TicketMessageConfig : IEntityTypeConfiguration<Domain.Models.Ticket.TicketMessage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Ticket.TicketMessage> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.Property(e => e.Message).IsRequired();

            builder.HasOne(e => e.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(e => e.TicketId);
        }
    }
}
