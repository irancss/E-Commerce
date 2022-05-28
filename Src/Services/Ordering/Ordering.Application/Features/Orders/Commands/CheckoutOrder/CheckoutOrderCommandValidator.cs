using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(c => c.UserName).NotEmpty().WithMessage("{UserName} is Required")
            .NotNull().MaximumLength(50).WithMessage("{UserName} Max Lenght is 50");


        RuleFor(c => c.EmailAddress).NotEmpty().WithMessage("{Email} is Required");


        RuleFor(c => c.TotalPrice).NotEmpty().WithMessage("{TotalPrice} is Required")
            .GreaterThan(0).WithMessage("{TotalPrice} Should be greater than zero");

    }
}