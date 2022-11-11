using Catalog.Application.CQRS.Seller.Command.CreateCommand;
using Catalog.Application.CQRS.Seller.Command.UpdateCommand;
using Catalog.Application.Validator.SellerValidator;
using FluentValidation.TestHelper;

namespace XUnitTests.ValidatorTests.Catalog
{
    public class SellerValidatorTests
    {
        private readonly CreateSellerCommandValidator _createValidator;
        private readonly UpdateSellerCommandValidator _updateValidator;

        public SellerValidatorTests()
        {
            _createValidator = new();
            _updateValidator = new();
        }

        [Fact]
        public async void CreateSellerCommandValidator_Validator_Should_have_error_when_SellerDescription_is_empty()
        {
            //Arrange
            var seller = new CreateSellerCommand() { SellerDescription = "", SellerName = "Alex Testing", SellerPhoneNumber = "+375295845555" };

            //Act
            var validate = await _createValidator.TestValidateAsync(seller);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.SellerDescription);
            validate.ShouldNotHaveValidationErrorFor(x => x.SellerName);
            validate.ShouldNotHaveValidationErrorFor(x => x.SellerPhoneNumber);
        }

        [Fact]
        public async void CreateSellerCommandValidator_Validator_Should_have_error_when_all_is_empty()
        {
            //Arrange
            var seller = new CreateSellerCommand() { SellerDescription = "", SellerName = "", SellerPhoneNumber = "" };

            //Act
            var validate = await _createValidator.TestValidateAsync(seller);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.SellerDescription);
            validate.ShouldHaveValidationErrorFor(x => x.SellerName);
            validate.ShouldHaveValidationErrorFor(x => x.SellerPhoneNumber);
        }

        [Fact]
        public async void UpdateSellerCommandValidator_Validator_Should_have_error_when_all_is_empty()
        {
            //Arrange
            var seller = new UpdateSellerCommand() { SellerDescription = "", SellerName = "", SellerPhoneNumber = "", Id = -1000 };

            //Act
            var validate = await _updateValidator.TestValidateAsync(seller);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.SellerDescription);
            validate.ShouldHaveValidationErrorFor(x => x.SellerName);
            validate.ShouldHaveValidationErrorFor(x => x.SellerPhoneNumber);
            validate.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public async void UpdateSellerCommandValidator_Validator_Should_have_error_when_SellerDescription_is_empty()
        {
            //Arrange
            var seller = new UpdateSellerCommand() { SellerDescription = "", SellerName = "Alex Aloha", SellerPhoneNumber = "+63463671223", Id = 1 };

            //Act
            var validate = await _updateValidator.TestValidateAsync(seller);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.SellerDescription);
            validate.ShouldNotHaveValidationErrorFor(x => x.SellerName);
            validate.ShouldNotHaveValidationErrorFor(x => x.SellerPhoneNumber);
            validate.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
