using Kidemy.Domain.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kidemy.Domain.Models.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kidemy.Infra.Data.Context.Configurations.Account
{
    public class AccountCodeConfig: IEntityTypeConfiguration<AccountCode>
    {
        public void Configure(EntityTypeBuilder<AccountCode> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.Property(a => a.Receiver).HasMaxLength(100);
            builder.Property(a => a.Code).HasMaxLength(15);
        }
    }
}
