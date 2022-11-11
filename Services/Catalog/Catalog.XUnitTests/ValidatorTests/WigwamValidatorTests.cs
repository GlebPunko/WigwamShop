using Catalog.Application.CQRS.Wigwam.Command.CreateCommand;
using Catalog.Application.CQRS.Wigwam.Command.UpdateCommand;
using Catalog.Application.Validator.WigwamValidator;
using FluentValidation.TestHelper;

namespace XUnitTests.ValidatorTests.Catalog
{
    public class WigwamValidatorTests
    {
        private readonly CreateWigwamCommandValidator _createValidator;
        private readonly UpdateWigwamCommandValidator _updateValidator;

        public WigwamValidatorTests()
        {
            _createValidator = new();
            _updateValidator = new();
        }

        [Fact]
        public async void CreateWigwamCommandValidator_Validator_Should_have_error_when_Description_is_empty()
        {
            //Arrange
            var wigwam = new CreateWigwamCommand()
            {
                Description = "",
                Price = 1000m,
                SellerInfoId = 1,
                Height = 10,
                Weight = 10,
                Width = 10,
                WigwamsName = "The best wigwam in the world"
            };

            //Act
            var validate = await _createValidator.TestValidateAsync(wigwam);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public async void CreateWigwamCommandValidator_Validator_Should_have_error_when_all_is_empty()
        {
            //Arrange
            var wigwam = new CreateWigwamCommand() { Description = "", Price = 0, SellerInfoId = 0, Height = 0, Weight = 0, Width = 0, WigwamsName = "" };

            //Act
            var validate = await _createValidator.TestValidateAsync(wigwam);

            //Assert
            validate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async void UpdateWigwamCommandValidator_Validator_Should_have_error_when_all_is_empty()
        {
            //Arrange
            var wigwam = new UpdateWigwamCommand()
            {
                Description = "",
                Price = 0,
                SellerInfoId = 0,
                Height = 0,
                Weight = 0,
                Width = 0,
                WigwamsName = "",
                Id = -1000
            };

            //Act
            var validate = await _updateValidator.TestValidateAsync(wigwam);

            //Assert
            validate.ShouldHaveAnyValidationError();
        }

        [Fact]
        public async void UpdateSellerCommandValidator_Validator_Should_have_error_when_SellerDescription_is_empty()
        {
            //Arrange
            var wigwam = new UpdateWigwamCommand()
            {
                Description = "",
                Price = 1000m,
                SellerInfoId = 1,
                Height = 10,
                Weight = 10,
                Width = 10,
                WigwamsName = "The best wigwam in the world"
            };

            //Act
            var validate = await _updateValidator.TestValidateAsync(wigwam);

            //Assert
            validate.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}
