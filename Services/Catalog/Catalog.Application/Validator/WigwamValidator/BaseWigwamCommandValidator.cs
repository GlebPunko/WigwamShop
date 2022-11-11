using Catalog.Application.CQRS.Wigwam.Command;
using FluentValidation;

namespace Catalog.Application.Validator.WigwamValidator
{
    public class BaseWigwamCommandValidator<T> : AbstractValidator<T>
        where T : BaseWigwamCommand
    {
        public BaseWigwamCommandValidator()
        {
            RuleFor(wigw => wigw.WigwamsName)
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(50);
            RuleFor(wigw => wigw.Description)
                .MinimumLength(20)
                .MaximumLength(200);

            RuleFor(wigw => wigw.Height)
                .NotEmpty();

            RuleFor(wigw => wigw.Price)
                .NotEmpty();

            RuleFor(wigw => wigw.Weight)
                .NotEmpty();

            RuleFor(wigw => wigw.Width)
                .NotEmpty();

            RuleFor(wigw => wigw.SellerInfoId)
                .NotEmpty();
        }
    }
}
