using Basket.Application.Services;
using Basket.Infastructure.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Basket.XUnitTests.Data;

namespace Basket.XUnitTests.ServiceTests
{
    public class PricaControlServiceTests
    {
        private readonly BasketDbContext _context;

        public PricaControlServiceTests()
        {
            var options = new DbContextOptionsBuilder<BasketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BasketDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void GetPricesSeller_Should_Return_Price_Info()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrders());
            await _context.SaveChangesAsync();
            const int id = 1;

            var priceControlService = new PriceControlService(_context);

            //Act
            var getResponse = await priceControlService.GetPricesSeller(id, CancellationToken.None);

            //Assert 
            getResponse.Should().NotBeNull();
            getResponse.Should().BeEquivalentTo("Seller with id = 1 price limit don`t exceeded!");
        }

        [Fact]
        public async void GetPricesSellers_Should_Return_List_Prices_Infos()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrders());
            await _context.SaveChangesAsync();

            var listForCompare = new List<string>()
            {
                "Seller with id = 1 price limit don`t exceeded!",
            };

            var priceControlService = new PriceControlService(_context);

            //Act
            var getResponse = await priceControlService.GetPricesSellers(CancellationToken.None);

            //Assert 
            getResponse.Should().NotBeNull();
            getResponse.Should().BeEquivalentTo(listForCompare);
        }
    }
}
