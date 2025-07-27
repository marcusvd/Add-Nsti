using FluentValidation;
using Application.Services.Operations.Customers.Dtos;
using Application.Services.Shared.DtoValidation;


namespace Application.Services.Operations.Customers.DtoValidation
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            //Commons
            RuleFor(xx => xx.Name).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(xx => xx.CNPJ).NotEmpty().NotNull().MinimumLength(8).MaximumLength(100);
            RuleFor(xx => xx.Description).MaximumLength(500);
            RuleFor(xx => xx.Registered).NotEmpty().NotNull();
            //Childrens
            RuleFor(xxAddress => xxAddress.Address).SetValidator(new AddressValidator());
            RuleFor(xxContact => xxContact.Contact).SetValidator(new ContactValidator());
            //Payment
            // RuleFor(xx => xx.Payment).NotEmpty()
            //  .NotNull().ScalePrecision(2, 8).GreaterThanOrEqualTo(1)
            // //  .When(xx => xx.Assured, ApplyConditionTo.AllValidators)
            //  .Equal(0).When(xx => !xx.Assured, ApplyConditionTo.AllValidators);
            // //Expiration
            // RuleFor(xx => xx.Expires).NotEmpty()
            //  .NotNull()
            //  .GreaterThanOrEqualTo(1).LessThanOrEqualTo(31)
            //  .When(xx => xx.Assured, ApplyConditionTo.CurrentValidator)
            //  .When(xx => xx.Assured, ApplyConditionTo.AllValidators)
            //  .Equal(0).When(xx => !xx.Assured, ApplyConditionTo.AllValidators);
            //PhysicallyMovingCosts
        }
    }
}


