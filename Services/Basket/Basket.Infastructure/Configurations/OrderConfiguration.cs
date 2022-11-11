using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Infastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.SellerId).IsRequired();

            builder.Property(x => x.Price).IsRequired().HasColumnType("money");

            builder.Property(x => x.CreateDate).IsRequired().HasDefaultValueSql("now()");
        }
    }
}
