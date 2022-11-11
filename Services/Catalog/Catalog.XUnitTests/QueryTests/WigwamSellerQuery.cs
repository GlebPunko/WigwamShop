using AutoMapper;
using Catalog.Application.CQRS.Wigwam.Query.GetAllQuery;
using Catalog.Application.CQRS.Wigwam.Query.GetByIdQuery;
using Catalog.XUnitTests.Helpers;
using Catalog.XUnitTests.Data;
using Catalog.Infastructure.Interfaces;
using Moq;

namespace Catalog.XUnitTests.QueryTests
{
    public class WigwamQueryTests
    {
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork = new();
        private readonly IMapper _mapper;

        public WigwamQueryTests()
        {
            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void GetAllWigwam_Should_Return_Wigwams()
        {
            //Arrange
            var wigwams = GetInfo.GetWigwams();

            _mockIUnitOfWork.Setup(x => x.Wigwam.GetAllAsync(new CancellationToken())).ReturnsAsync(wigwams);
            var query = new GetAllQueryHandler(_mockIUnitOfWork.Object, _mapper);

            //Act
            var getAllWigwams = await query.Handle(_mapper.Map<GetAllQuery>(wigwams.Find(x =>
                x.Id == 1)), new CancellationToken());

            //Assert
            Assert.NotNull(getAllWigwams);
            Assert.Equal(wigwams.Count, getAllWigwams.Count);
        }

        [Fact]
        public async void GetByIdWigwam_QueryHandler_Should_Return_Wigwam()
        {
            //Arrange
            var wigwam = GetInfo.GetWigwams().Find(x => x.Id == 1);

            _mockIUnitOfWork.Setup(x => x.Wigwam.GetAsync(wigwam!.Id, new CancellationToken()))!
                .ReturnsAsync(wigwam);
            var query = new GetWigwamByIdQueryHandler(_mockIUnitOfWork.Object, _mapper);

            //Act
            var getWigwam = await query.Handle(_mapper.Map<GetWigwamByIdQuery>(wigwam), new CancellationToken());

            //Assert
            Assert.NotNull(getWigwam);
            Assert.Equal(wigwam!.Price, getWigwam.Price);
            Assert.Equal(wigwam.SellerInfoId, getWigwam.SellerInfoId);
        }

    }
}
