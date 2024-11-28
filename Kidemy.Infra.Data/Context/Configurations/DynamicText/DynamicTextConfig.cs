using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kidemy.Infra.Data.Context.Configurations.DynamicText
{
    public class DynamicTextConfig : IEntityTypeConfiguration<Domain.Models.DynamicText.DynamicText>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.DynamicText.DynamicText> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasQueryFilter(d => !d.IsDeleted);

        }
    }
}
