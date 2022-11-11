using Catalog.Application.CQRS.Seller.Command.CreateCommand;

namespace Catalog.Application.Validator.SellerValidator
{
    public class CreateSellerCommandValidator : BaseSellerCommandValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator() : base()
        { }
    }
}
