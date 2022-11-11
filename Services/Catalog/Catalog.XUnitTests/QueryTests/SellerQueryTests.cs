using AutoMapper;
using Catalog.Application.CQRS.Seller.Command.CreateCommand;
using Catalog.Application.CQRS.Seller.Query.GetAllQuery;
using Catalog.Application.CQRS.Seller.Query.GetByIdQuery;
using Catalog.XUnitTests.Data;
using Catalog.XUnitTests.Helpers;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using Moq;

namespace Catalog.XUnitTests.QueryTests
{
    public class SellerQueryTests
    {
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork = new();
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSellerCommand> _createValidator;

        public SellerQueryTests()
        {
            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void GetAllSellerQueryHandler_Should_Return_Sellers()
        {
            //Arrange
            var sellers = GetInfo.GetSellers();

            _mockIUnitOfWork.Setup(x => x.Seller.GetAllAsync(new CancellationToken()))
                .ReturnsAsync(sellers);
            var query = new GetAllSellerQueryHandler(_mockIUnitOfWork.Object, _mapper);

            //Act
            var getAllSeller = await query.Handle(_mapper.Map<GetAllSellerQuery>(sellers.Find(x => x.Id == 1)),
                new CancellationToken());

            //Assert
            Assert.NotNull(getAllSeller);
            Assert.Equal(sellers.Count, getAllSeller.Count);
        }

        [Fact]
        public async void GetByIdSellerQueryHandler_Should_Return_Seller()
        {
            //Arrange
            var seller = GetInfo.GetSellers().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Seller.GetAsync(seller.Id, new CancellationToken()))!
                .ReturnsAsync(seller);
            var query = new GetByIdQuerySellerHandler(_mockIUnitOfWork.Object, _mapper);

            //Act
            var getSeller = await query.Handle(_mapper.Map<GetByIdSellerQueryHandler>(seller), new CancellationToken());

            //Assert
            Assert.NotNull(getSeller);
            Assert.Equal(seller!.SellerName, getSeller.SellerName);
            Assert.Equal(seller!.SellerPhoneNumber, getSeller.SellerPhoneNumber);
            Assert.Equal(seller.SellerDescription, getSeller.SellerDescription);
        }
    }
}
