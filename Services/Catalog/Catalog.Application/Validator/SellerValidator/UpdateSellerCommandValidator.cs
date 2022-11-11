using Catalog.Application.CQRS.Seller.Command.UpdateCommand;
using FluentValidation;

namespace Catalog.Application.Validator.SellerValidator
{
    public class UpdateSellerCommandValidator : BaseSellerCommandValidator<UpdateSellerCommand>
    {
        public UpdateSellerCommandValidator() : base()
        {
            RuleFor(seller => seller.Id)
                .NotEmpty()
                .Must(CheckId);
        }

        private bool CheckId(int id) => id > 0;
    }
}
