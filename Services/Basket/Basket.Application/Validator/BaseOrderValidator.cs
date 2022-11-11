using Basket.Application.Models;
using FluentValidation;

namespace Basket.Application.Validator
{
    public class BaseOrderValidator<T> : AbstractValidator<T>
        where T : BaseOrderModel
    {
        public BaseOrderValidator()
        {
            RuleFor(order => order.SellerId)
                .NotEmpty();

            RuleFor(order => order.Price)
                .NotEmpty();
        }
    }
}
