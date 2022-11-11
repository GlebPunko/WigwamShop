using Basket.Application.Models;
using Basket.Application.Validator;
using FluentValidation.TestHelper;

namespace Basket.XUnitTests.ValidatorTests
{
    public class BasketValidatorTests
    {
        private readonly CreateOrderValidator _createOrderValidator;
        private readonly UpdateOrderValidator _updateOrderValidator;

        public BasketValidatorTests()
        {
            _createOrderValidator = new();
            _updateOrderValidator = new();
        }

        [Fact]
        public void CreateOrderValidator_Should_have_error_when_SellerId_and_Price_is_empty()
        {
            // Arrange
            var model = new CreateOrderModel() { Price = 0, SellerId = 0 };
            // Act
            var validate = _createOrderValidator.TestValidate(model);
            // Assert
            validate.ShouldHaveValidationErrorFor(x => x.Price);
            validate.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        [Fact]
        public void UpdateOrderValidator_Should_have_error_when_SellerId_and_Price_and_Id_is_empty()
        {
            // Arrange
            var model = new UpdateOrderModel() { Price = 0, SellerId = 0, Id = 0 };
            // Act
            var validate = _updateOrderValidator.TestValidate(model);
            // Assert
            validate.ShouldHaveValidationErrorFor(x => x.Price);
            validate.ShouldHaveValidationErrorFor(x => x.SellerId);
            validate.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void CreateOrderValidator_Should_have_error_when_SellerId_is_empty()
        {
            // Arrange
            var model = new CreateOrderModel() { SellerId = 0 };
            //Act
            var validate = _createOrderValidator.TestValidate(model);
            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        [Fact]
        public void UpdateOrderValidator_Should_have_error_when_Id_is_empty()
        {
            // Arrange
            var model = new UpdateOrderModel() { Id = 0 };
            //Act
            var validate = _updateOrderValidator.TestValidate(model);
            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
