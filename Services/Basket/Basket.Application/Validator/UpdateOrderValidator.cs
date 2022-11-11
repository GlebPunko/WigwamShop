using Basket.Application.Models;
using FluentValidation;

namespace Basket.Application.Validator
{
    public class UpdateOrderValidator : BaseOrderValidator<UpdateOrderModel>
    {
        public UpdateOrderValidator() : base()
        {
            RuleFor(order => order.Id)
                .NotEmpty();
        }
    }
}
