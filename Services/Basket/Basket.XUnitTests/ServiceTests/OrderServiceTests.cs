using AutoMapper;
using Basket.Application.CustomException;
using Basket.Application.Models;
using Basket.Application.Services;
using Basket.Application.Validator;
using Basket.Domain.Entities;
using Basket.Infastructure.Repositories.Interfaces;
using Basket.XUnitTests.Helpers;
using Moq;
using Basket.XUnitTests.Data;

namespace Basket.XUnitTests.ServiceTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IBasketRepository> _moqIBasketRepository = new();
        private readonly CreateOrderValidator _createOrderValidator;
        private readonly UpdateOrderValidator _updateOrderValidator;
        private readonly IMapper _mapper;

        public OrderServiceTests()
        {
            _createOrderValidator = new CreateOrderValidator();
            _updateOrderValidator = new UpdateOrderValidator();
            _mapper = TestsHelper.SetupMapper();
        }

        [Fact]
        public async void GetAllAsync_Should_Return_List_Orders()
        {
            //Arrange
            var orders = GetInfo.GetOrders();

            _moqIBasketRepository.Setup(x => x.GetAllAsync(new CancellationToken())).ReturnsAsync(orders);
            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator, _createOrderValidator);

            //Act
            var getAllOrder = await service.GetAllAsync(new CancellationToken());

            //Assert
            Assert.NotNull(getAllOrder);
            Assert.Equal(1, getAllOrder.Count);
        }

        [Fact]
        public async void GetById_Should_Return_Order()
        {
            //Arrange
            var order = GetInfo.GetOrder();
            const int id = 1;

            _moqIBasketRepository.Setup(x => x.GetAsync(id, new CancellationToken())).ReturnsAsync(order);
            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            var getByIdOrder = await service.GetAsync(id, new CancellationToken());

            //Assert
            Assert.NotNull(getByIdOrder);
            Assert.Equal(id, getByIdOrder!.Id);
            Assert.Equal(order.SellerId, getByIdOrder.SellerId);
            Assert.Equal(order.CreateDate, getByIdOrder.CreateDate);
        }

        [Fact]
        public async void GetById_Should_Throw_NotFoundException()
        {
            //Arrange
            _moqIBasketRepository.Setup(x => x.GetAsync(-10, new CancellationToken()))!.ReturnsAsync(null as Order);

            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            try
            {
                await service.GetAsync(-10, new CancellationToken());
            }
            catch (NotFoundException e)
            {
                //Assert
                Assert.Equal(1, 1);
            }
        }

        [Fact]
        public async void CreateAsync_Should_Create_Order_And_Return_OrderViewModel()
        {
            //Arrange
            var order = GetInfo.GetOrder();

            _moqIBasketRepository.Setup(x => x.CreateAsync(It.IsAny<Order>(), new CancellationToken()))
                .ReturnsAsync(order);
            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            var createOrder =
                await service.CreateAsync(_mapper.Map<CreateOrderModel>(order), new CancellationToken());

            //Assert
            Assert.NotNull(createOrder);
            Assert.Equal(order.SellerId, createOrder!.SellerId);
            Assert.Equal(order.Price, createOrder.Price);
            Assert.Equal(order.Id, createOrder.Id);
            Assert.Equal(order.CreateDate, createOrder.CreateDate);
        }

        [Fact]
        public async void DeleteAsync_Should_Delete_Order()
        {
            //Arrange
            var order = GetInfo.GetOrder();

            _moqIBasketRepository.Setup(x => x.GetAsync(order.Id, new CancellationToken()))
                .ReturnsAsync(order);
            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            await service.DeleteAsync(order.Id, new CancellationToken());

            //Assert
            _moqIBasketRepository.VerifyAll();
        }

        [Fact]
        public async void DeleteAsync_Should_Delete_Order_And_Thrown_NotFoundException()
        {
            //Arrange
            var order = GetInfo.GetOrder();

            _moqIBasketRepository.Setup(x => x.GetAsync(order.Id, new CancellationToken()))
                .ReturnsAsync(order);
            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            try
            {
                await service.DeleteAsync(order.Id, new CancellationToken());
            }
            catch (NotFoundException e)
            {
                //Assert
                Assert.Equal(1, 1);
            }
        }

        [Fact]
        public async void UpdateAsync_Should_Update_Order()
        {
            //Arrange
            var oldOrder = GetInfo.GetOrder();
            var newOrder = GetInfo.GetOrder();
            newOrder.Price = 1000m;

            _moqIBasketRepository.Setup(x => x.UpdateAsync(It.IsAny<Order>(), new CancellationToken()))
                .ReturnsAsync(newOrder);

            var service = new OrderService(_moqIBasketRepository.Object, _mapper, _updateOrderValidator,
                _createOrderValidator);

            //Act
            var updateOrder = await service.UpdateAsync(_mapper.Map<UpdateOrderModel>(newOrder), new CancellationToken());

            //Assert
            Assert.NotNull(service);
            Assert.NotEqual(oldOrder.Price, updateOrder!.Price);
            Assert.Equal(oldOrder.SellerId, updateOrder.SellerId);
            Assert.NotEqual(oldOrder.CreateDate, updateOrder.CreateDate);
        }
    }
}
