using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infastructure.Configurations
{
    public class SellerInfoConfiguration : IEntityTypeConfiguration<SellerInfo>
    {
        public void Configure(EntityTypeBuilder<SellerInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.SellerDescription).IsRequired().HasMaxLength(200);

            builder.Property(x => x.SellerName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.SellerPhoneNumber).IsRequired().HasMaxLength(20);
        }
    }
}
