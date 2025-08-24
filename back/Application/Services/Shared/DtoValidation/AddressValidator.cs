// using FluentValidation;
// using Application.Services.Shared.Dtos;

// namespace Application.Services.Shared.DtoValidation
// {
//     public class AddressValidator : AbstractValidator<AddressDto>
//     {
//         public AddressValidator()
//         {
//            RuleFor(xx => xx.ZipCode).MaximumLength(150);
//            RuleFor(xx => xx.Street).NotEmpty().NotNull().MaximumLength(150);
//            RuleFor(xx => xx.Number).NotEmpty().NotNull().MaximumLength(15);
//            RuleFor(xx => xx.District).NotEmpty().NotNull().MaximumLength(150);
//            RuleFor(xx => xx.City).NotEmpty().NotNull().MaximumLength(150);
//            RuleFor(xx => xx.State).NotEmpty().NotNull().MaximumLength(3);
//            RuleFor(xx => xx.Complement).MaximumLength(500);
//         }
//     }
// }