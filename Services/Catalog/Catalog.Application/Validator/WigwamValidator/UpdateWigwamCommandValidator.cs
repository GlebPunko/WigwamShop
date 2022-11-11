using Catalog.Application.CQRS.Wigwam.Command.UpdateCommand;
using FluentValidation;

namespace Catalog.Application.Validator.WigwamValidator
{
    public class UpdateWigwamCommandValidator : BaseWigwamCommandValidator<UpdateWigwamCommand>
    {
        public UpdateWigwamCommandValidator() : base()
        {
            RuleFor(wigw => wigw.Id)
                .NotEmpty()
                .Must(CheckId);
        }

        private bool CheckId(int id) => id > 0;
    }
}
