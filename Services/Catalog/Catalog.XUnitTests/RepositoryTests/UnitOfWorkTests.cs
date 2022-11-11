using Catalog.Infastructure.Context;
using Catalog.Infastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Catalog.XUnitTests.Data;

namespace XUnitTests.RepositoryTests.Catalog
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly CatalogDbContext _context;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CatalogDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void GetAllAsync_Should_Return_List_Sellers_Collection()
        {
            //Arrange
            await _context.SellerInfos.AddRangeAsync(GetInfo.GetSellers());
            await _context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var getAllSellers = await unitOfWork.Seller.GetAllAsync(CancellationToken.None);

            //Assert 
            getAllSellers.Should().HaveCount(GetInfo.GetSellers().Count);
        }

        [Fact]
        public async void GetAllAsync_Should_Return_List_Wigwams_Collection()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var getAllWigwams = await unitOfWork.Wigwam.GetAllAsync(CancellationToken.None);

            //Assert 
            getAllWigwams.Should().HaveCount(GetInfo.GetWigwams().Count);
        }

        [Fact]
        public async void GetByIdAsync_Should_Return_Seller()
        {
            //Arrange
            await _context.SellerInfos.AddRangeAsync(GetInfo.GetSellers());
            await _context.SaveChangesAsync();
            const int id = 1;

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var getByIdAsync = await unitOfWork.Seller.GetAsync(id, CancellationToken.None);

            //Assert
            getByIdAsync.Should().NotBeNull();
            getByIdAsync.Should().BeEquivalentTo(GetInfo.GetSellers().Find(x => x.Id == id));
        }

        [Fact]
        public async void GetByIdAsync_Should_Return_Wigwam()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();
            const int id = 1;

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var getByIdAsync = await unitOfWork.Wigwam.GetAsync(id, CancellationToken.None);

            //Assert
            getByIdAsync.Should().NotBeNull();
            getByIdAsync.Should().BeEquivalentTo(GetInfo.GetWigwams().Find(x => x.Id == id),
                e => e.Excluding(x => x.SellerInfo));
        }

        [Fact]
        public async void CreateAsync_Should_Return_Created_Seller()
        {
            //Arrange
            var seller = GetInfo.GetSellers().Find(x => x.Id == 1);

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var createSeller = await unitOfWork.Seller.CreateAsync(seller, CancellationToken.None);

            //Assert
            createSeller.Should().NotBeNull();
            createSeller.Should().BeEquivalentTo(seller);
        }

        [Fact]
        public async void CreateAsync_Should_Return_Created_Wigwam()
        {
            //Arrange
            var wigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var createWigwam = await unitOfWork.Wigwam.CreateAsync(wigwam!, CancellationToken.None);

            //Assert
            createWigwam.Should().NotBeNull();
            createWigwam.Should().BeEquivalentTo(wigwam, x =>
                x.Excluding(e => e!.SellerInfo));
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Sellers_Id()
        {
            //Arrange
            await _context.SellerInfos.AddRangeAsync(GetInfo.GetSellers());
            await _context.SaveChangesAsync();

            const int id = 1;
            var seller = await _context.SellerInfos.FirstOrDefaultAsync(x => x.Id == id);

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var deleteSeller = await unitOfWork.Seller.DeleteAsync(seller!, CancellationToken.None);

            //Assert
            deleteSeller.Should().Be(id);
        }

        [Fact]
        public async void DeleteAsync_Should_Return_Wigwams_Id()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();

            const int id = 1;
            var wigwam = await _context.WigwamsInfos.FirstOrDefaultAsync(x => x.Id == id);

            var unitOfWork = new UnitOfWork(_context);

            //Act
            var deleteWigwam = await unitOfWork.Wigwam.DeleteAsync(wigwam!, CancellationToken.None);

            //Assert
            deleteWigwam.Should().Be(id);
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Updated_Seller()
        {
            //Arrange
            await _context.SellerInfos.AddRangeAsync(GetInfo.GetSellers());
            await _context.SaveChangesAsync();

            const int id = 1;
            var sellerOld = await _context.SellerInfos.FirstOrDefaultAsync(x => x.Id == id);
            var sellerNew = await _context.SellerInfos.FirstOrDefaultAsync(x => x.Id == id);
            sellerNew!.SellerDescription = "New new new new new";

            var unitOfWork = new UnitOfWork(_context);
            //Act
            var updateSeller = await unitOfWork.Seller.UpdateAsync(sellerNew!, CancellationToken.None);

            //Assert
            updateSeller.Should().BeEquivalentTo(sellerOld, x =>
                x.Excluding(e => e.SellerDescription));
        }

        [Fact]
        public async void UpdateAsync_Should_Return_Updated_Wigwam()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();

            const int id = 1;
            var wigwamOld = await _context.WigwamsInfos.FirstOrDefaultAsync(x => x.Id == id);
            var wigwamNew = await _context.WigwamsInfos.FirstOrDefaultAsync(x => x.Id == id);
            wigwamNew!.Description = "New new new new new new";

            var unitOfWork = new UnitOfWork(_context);
            //Act
            var updateSeller = await unitOfWork.Wigwam.UpdateAsync(wigwamNew!, CancellationToken.None);

            //Assert
            updateSeller.Should().BeEquivalentTo(wigwamOld, x =>
                x.Excluding(e => e!.Description));
        }

        [Fact]
        public async void SaveAsync_Should_Save_My_Info()
        {
            //Arrange
            var wigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);

            var unitOfWork = new UnitOfWork(_context);

            var createWigwam = unitOfWork.Wigwam.CreateAsync(wigwam!, CancellationToken.None);

            //Act
            await unitOfWork.SaveAsync(CancellationToken.None);

            //Assert
            var result = await unitOfWork.Wigwam.GetAsync(wigwam!.Id, CancellationToken.None);
            result.Should().BeEquivalentTo(wigwam, x =>
                x.Excluding(e => e.SellerInfo));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
