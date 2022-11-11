using Basket.Domain.Entities;
using Basket.Infastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infastructure
{
    public class DataSeeder
    {
        private readonly BasketDbContext _context;

        public DataSeeder(BasketDbContext context)
        {
            _context = context;
        }

        private static IEnumerable<Order> GetOrders() => new List<Order>()
        {
            new()
            {
                SellerId = 1,
                Price = 100.22m,
                CreateDate = DateTimeOffset.Now,
            },
            new()
            {
                SellerId = 2,
                Price = 1234.56m,
                CreateDate = DateTimeOffset.Now
            },
        };

        private static IEnumerable<Seller> GetSellers() => new List<Seller>()
        {
            new()
            {
                Name = "seller first",
                PhoneNumber = "+375 29 121 00 11",
                Email = "seller_first@gmail.com"
            },
            new()
            {
                Name = "seller second",
                PhoneNumber = "+375 17 987 12 77",
                Email = "seller_second@gmail.com"
            },
        };

        public async Task InitializeDBAsync()
        {
            if(!await _context.Sellers.AnyAsync())
            {
                await _context.AddRangeAsync(GetSellers());
                await _context.SaveChangesAsync();
            }

            if(!await _context.Orders.AnyAsync())
            {
                await _context.AddRangeAsync(GetOrders());
                await _context.SaveChangesAsync();
            }
        }
    }
}
