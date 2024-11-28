using Barnamenevisan.Localizing.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kidemy.Infra.Data.Context
{
    public class KidemyContext : DbContext
    {
        public KidemyContext(DbContextOptions<KidemyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RegisterLocalizing();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KidemyContext).Assembly);
            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);

        }

    }
}
