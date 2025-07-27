using FluentValidation;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Shared.DtoValidation;

namespace Application.Services.Operations.Authentication.DtoValidation
{
    public class UserAccountValidator : AbstractValidator<UserAccountDto>
    {
        public UserAccountValidator()
        {
            //Commons
//             RuleFor(xx => xx.UserName).NotEmpty().NotNull().MaximumLength(150);
//             RuleFor(xx => xx.Email).NotEmpty().NotNull().MaximumLength(200);
//            //can't to be make via here
//            //RuleFor(xx => xx.Password).NotEmpty().NotNull().MaximumLength(200);
//             RuleFor(xx => xx.Company.Name).NotEmpty().NotNull().MaximumLength(200);
//  //Childrens
            RuleFor(xxAddress => xxAddress.Address).SetValidator(new AddressValidator());
            RuleFor(xxContact => xxContact.Contact).SetValidator(new ContactValidator());

            
        }
    }
}


