using Catalog.Application.CQRS.Seller.Command;
using FluentValidation;

namespace Catalog.Application.Validator.SellerValidator
{
    public class BaseSellerCommandValidator<T> : AbstractValidator<T>
        where T : BaseSellerCommand
    {
        public BaseSellerCommandValidator()
        {
            RuleFor(seller => seller.SellerName)
               .NotNull()
               .MinimumLength(10)
               .MaximumLength(50);

            RuleFor(seller => seller.SellerDescription)
                .NotNull()
                .MinimumLength(20)
                .MaximumLength(200);

            RuleFor(seller => seller.SellerPhoneNumber)
                .NotNull()
                .MinimumLength(9)
                .MaximumLength(15);
        }
    }
}
