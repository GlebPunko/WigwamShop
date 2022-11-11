using AutoMapper;
using Catalog.Application.CQRS.Wigwam.Command.CreateCommand;
using Catalog.Application.CQRS.Wigwam.Command.DeleteCommand;
using Catalog.Application.CQRS.Wigwam.Command.UpdateCommand;
using Catalog.Domain.Entities;
using Catalog.XUnitTests.Data;
using Catalog.XUnitTests.Helpers;
using Catalog.Infastructure.Interfaces;
using FluentValidation;
using FluentAssertions;
using Moq;

namespace Catalog.XUnitTests.CommandTests
{
    public class WigwamCommandTests
    {
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork = new();
        private readonly IMapper _mapper;
        private readonly IValidator<CreateWigwamCommand> _createValidator;
        private readonly IValidator<UpdateWigwamCommand> _updateValidator;

        public WigwamCommandTests()
        {
            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void CreateWigwam_Should_Create_Wigwam_And_Return_His()
        {
            //Arrange
            var wigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Wigwam.CreateAsync(It.IsAny<WigwamsInfo>(), CancellationToken.None))!
                .ReturnsAsync(wigwam);

            var command = new CreateWigwamCommandHandler(_mapper, _createValidator, _mockIUnitOfWork.Object);

            //Act
            var createWigwam = await command.Handle(_mapper.Map<CreateWigwamCommand>(wigwam), CancellationToken.None);

            //Assert
            Assert.NotNull(createWigwam);
            createWigwam.Should().BeEquivalentTo(wigwam, x =>
                x.Excluding(e => e!.SellerInfo));
            //Assert.Equal(wigwam!.Price, createSeller.Price);
            //Assert.Equal(wigwam.Description, createSeller.Description);
            //Assert.Equal(wigwam.Height, createSeller.Height);
        }

        [Fact]
        public async void DeleteWigwam_Should_Delete_And_Return_Its_Id()
        {
            //Arrange
            var wigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Wigwam.GetAsync(wigwam!.Id, CancellationToken.None))!
                .ReturnsAsync(wigwam);
            _mockIUnitOfWork.Setup(x => x.Wigwam.DeleteAsync(wigwam!, CancellationToken.None))
                .ReturnsAsync(wigwam!.Id);

            var command = new DeleteWigwamByIdCommandHandler(_mockIUnitOfWork.Object);

            //Act
            var deleteSeller = await command.Handle(_mapper.Map<DeleteWigwamByIdCommand>(wigwam), CancellationToken.None);

            //Assert
            Assert.Equal(wigwam!.Id, deleteSeller);
            _mockIUnitOfWork.VerifyAll();
        }

        [Fact]
        public async void UpdateWigwam_Should_Update_Wigwam()
        {
            //Arrange
            var oldWigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);
            var newWigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);
            newWigwam!.WigwamsName = "New test name";

            _mockIUnitOfWork.Setup(x => x.Seller.GetAsync(newWigwam.SellerInfoId, CancellationToken.None))!
                .ReturnsAsync(GetInfo.GetSellers().Find(x => x.Id == newWigwam.SellerInfoId));
            _mockIUnitOfWork.Setup(x => x.Wigwam.UpdateAsync(It.IsAny<WigwamsInfo>(), CancellationToken.None))
                .ReturnsAsync(newWigwam);

            var command = new UpdateWigwamCommandHandler(_updateValidator, _mapper, _mockIUnitOfWork.Object);

            //Act
            var updateWigwam = await command.Handle(_mapper.Map<UpdateWigwamCommand>(newWigwam), CancellationToken.None);

            //Assert
            Assert.NotNull(updateWigwam);
            Assert.NotEqual(oldWigwam!.WigwamsName, updateWigwam.WigwamsName);

            updateWigwam.Should()
                .BeEquivalentTo(oldWigwam, x =>
                    x.Excluding(e => e!.SellerInfo)
                        .Excluding(e => e!.WigwamsName));
        }
    }
}
