using Basket.Domain.Entities;

namespace Basket.XUnitTests.Data
{
    internal class GetInfo
    {
        public static List<Order> GetOrders() => new()
        {
            new Order()
            {
                CreateDate = DateTimeOffset.Now, Id = 1, Price = 20m,
                Seller = new Seller()
                    { Email = "hxdfkyshf@gmail.com", Id = 1, Name = "Alexander", PhoneNumber = "+375296754322" },
                SellerId = 1
            }
        };

        public static Order GetOrder() => new()
        {
            CreateDate = DateTimeOffset.Now,
            Id = 1,
            Price = 20m,
            Seller = new Seller()
            { Email = "hxdfkyshf@gmail.com", Id = 1, Name = "Alexander", PhoneNumber = "+375296754322" },
            SellerId = 1
        };
    }
}
