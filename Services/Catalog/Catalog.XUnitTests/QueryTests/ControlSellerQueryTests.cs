using AutoMapper;
using Catalog.Application.CQRS.ControlSeller.Query.ControlAllSellersQuery;
using Catalog.Application.CQRS.ControlSeller.Query.ControlByIdQuery;
using Catalog.Infastructure.Context;
using Catalog.XUnitTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Catalog.XUnitTests.Data;

namespace XUnitTests.ServiceTests.Catalog.Query
{
    public class ControlSellerQueryTests
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;

        public ControlSellerQueryTests()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CatalogDbContext(options);

            _context.Database.EnsureCreated();

            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void ControlAllSellers_Should_Return_Count_Wigwams_Infos()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();

            var listForCompare = new List<string>()
            {
                "Seller with id = 1 wigwams count limit don`t exceeded!",
                "Seller with id = 2 wigwams count limit don`t exceeded!"
            };

            var priceControlService = new ControlAllSellersQueryHandler(_context);

            //Act
            var getResponse = await priceControlService.Handle(_mapper.Map<ControlAllSellersQuery>(null!),
                CancellationToken.None);

            //Assert 
            getResponse.Should().NotBeNull();
            getResponse.Should().BeEquivalentTo(listForCompare);
        }

        [Fact]
        public async void ControlByIdSeller_Should_Return_Count_Wigwams_Info()
        {
            //Arrange
            await _context.WigwamsInfos.AddRangeAsync(GetInfo.GetWigwams());
            await _context.SaveChangesAsync();
            const int id = 1;

            var priceControlService = new ControlByIdSellerQueryHandler(_context);
            var testModel = new ControlByIdSellerQuery(id);

            //Act
            var getResponse = await priceControlService.Handle(testModel, CancellationToken.None);

            //Assert
            getResponse.Should().NotBeNull();
            getResponse.Should()
                .BeEquivalentTo("The maximum number of wigwams for the seller with id = 1 has not been exceeded!");
        }
    }
}
