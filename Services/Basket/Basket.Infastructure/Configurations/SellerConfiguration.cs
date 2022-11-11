using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infastructure.Configurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
        }
    }
}
