using Basket.Infastructure.Context;
using Basket.Infastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Basket.XUnitTests.Data;

namespace Basket.XUnitTests.RepositoryTests
{
    public class BasketRepositoryTests : IDisposable
    {
        private readonly BasketDbContext _context;

        public BasketRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<BasketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new BasketDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void GetAllAsync_Should_Return_List_Orders_Collection()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrders());
            await _context.SaveChangesAsync();

            var basketRepository = new BasketRepository(_context);

            //Act
            var getAllOrders = await basketRepository.GetAllAsync(CancellationToken.None);

            //Assert 
            getAllOrders.Should().HaveCount(GetInfo.GetOrders().Count);
        }

        [Fact]
        public async void GetByIdAsync_Should_Return_Order()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrder());
            await _context.SaveChangesAsync();
            const int id = 1;

            var basketRepository = new BasketRepository(_context);

            //Act
            var getByIdAsync = await basketRepository.GetAsync(id, CancellationToken.None);

            //Assert
            getByIdAsync.Should().NotBeNull();
            getByIdAsync.Should().BeEquivalentTo(GetInfo.GetOrder(), x =>
                x.Excluding(e => e.Seller)
                    .Excluding(e => e.CreateDate));
        }

        [Fact]
        public async void CreateAsync_Should_Return_Created_Order()
        {
            //Arrange
            var order = GetInfo.GetOrder();

            var basketRepository = new BasketRepository(_context);

            //Act
            var createWigwam = await basketRepository.CreateAsync(order!, CancellationToken.None);

            //Assert
            createWigwam.Should().NotBeNull();
            createWigwam.Should().BeEquivalentTo(order, x =>
                x.Excluding(e => e!.Seller));
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Order_Id()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrders());
            await _context.SaveChangesAsync();

            const int id = 1;
            var seller = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            var basketRepository = new BasketRepository(_context);

            //Act
            var deleteSeller = await basketRepository.DeleteAsync(seller!, CancellationToken.None);

            //Assert
            deleteSeller.Should().Be(id);
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Updated_Order()
        {
            //Arrange
            await _context.Orders.AddRangeAsync(GetInfo.GetOrders());
            await _context.SaveChangesAsync();

            const int id = 1;
            var wigwamOld = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            var wigwamNew = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            wigwamNew!.Price = 2000000m;

            var basketRepository = new BasketRepository(_context);
            //Act
            var updateSeller = await basketRepository.UpdateAsync(wigwamNew!, CancellationToken.None);

            //Assert
            updateSeller.Should().BeEquivalentTo(wigwamOld, x =>
                x.Excluding(e => e!.Price));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
