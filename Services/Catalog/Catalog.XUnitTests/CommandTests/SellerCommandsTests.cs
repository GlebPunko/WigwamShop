using AutoMapper;
using Catalog.Application.CQRS.Seller.Command.CreateCommand;
using Catalog.Application.CQRS.Seller.Command.DeleteCommand;
using Catalog.Application.CQRS.Seller.Command.UpdateCommand;
using Catalog.Domain.Entities;
using Catalog.XUnitTests.Helpers;
using Catalog.XUnitTests.Data;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using Moq;

namespace Catalog.XUnitTests.CommandTests
{
    public class SellerCommandTests
    {
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork = new();
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSellerCommand> _createValidator;
        private readonly IValidator<UpdateSellerCommand> _updateValidator;

        public SellerCommandTests()
        {
            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void CreateSeller_Should_Create_Seller_And_Return_His()
        {
            //Arrange
            var seller = GetInfo.GetSellers().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Seller.CreateAsync(It.IsAny<SellerInfo>(), CancellationToken.None))!
                .ReturnsAsync(seller);

            var command = new CreateSellerCommandHandler(_mockIUnitOfWork.Object, _mapper, _createValidator);

            //Act
            var createSeller = await command.Handle(_mapper.Map<CreateSellerCommand>(seller), CancellationToken.None);

            //Assert
            Assert.NotNull(createSeller);
            Assert.Equal(seller!.SellerName, createSeller.SellerName);
            Assert.Equal(seller.SellerDescription, createSeller.SellerDescription);
            Assert.Equal(seller.SellerPhoneNumber, createSeller.SellerPhoneNumber);
        }

        [Fact]
        public async void DeleteSeller_Should_Delete_Seller()
        {
            //Arrange
            var seller = GetInfo.GetSellers().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Seller.GetAsync(seller!.Id, CancellationToken.None))!
                .ReturnsAsync(seller);
            _mockIUnitOfWork.Setup(x => x.Seller.DeleteAsync(seller!, CancellationToken.None))
                .ReturnsAsync(seller!.Id);

            var command = new DeleteSellerCommandHandler(_mockIUnitOfWork.Object);

            //Act
            var deleteSeller = await command.Handle(_mapper.Map<DeleteSellerCommand>(seller), CancellationToken.None);

            //Assert
            Assert.Equal(seller!.Id, deleteSeller);
            _mockIUnitOfWork.VerifyAll();
        }

        [Fact]
        public async void UpdateSeller_Should_Update_Seller()
        {
            //Arrange
            var oldSeller = GetInfo.GetSellers().Find(x => x.Id == 1);
            var newSeller = GetInfo.GetSellers().Find(x => x.Id == 1);
            newSeller!.SellerName = "New test name";

            _mockIUnitOfWork.Setup(x => x.Seller.UpdateAsync(It.IsAny<SellerInfo>(), CancellationToken.None))
                .ReturnsAsync(newSeller);

            var command = new UpdateSellerCommandHandler(_mockIUnitOfWork.Object, _updateValidator, _mapper);

            //Act
            var updateSeller = await command.Handle(_mapper.Map<UpdateSellerCommand>(newSeller), CancellationToken.None);

            //Assert
            Assert.NotNull(updateSeller);
            Assert.NotEqual(oldSeller!.SellerName, updateSeller.SellerName);
            Assert.Equal(oldSeller.SellerPhoneNumber, updateSeller.SellerPhoneNumber);
            Assert.Equal(oldSeller.SellerDescription, updateSeller.SellerDescription);
        }
    }
}
