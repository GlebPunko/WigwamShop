using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infastructure.Configurations
{
    public class WigwamInfoConfiguration : IEntityTypeConfiguration<WigwamsInfo>
    {
        public void Configure(EntityTypeBuilder<WigwamsInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Height).IsRequired();

            builder.Property(x => x.Price).IsRequired().HasColumnType("money");

            builder.Property(x => x.Weight).IsRequired();

            builder.Property(x => x.Width).IsRequired();

            builder.Property(x => x.WigwamsName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        }
    }
}
