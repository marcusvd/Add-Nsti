// using FluentValidation;
// using Application.Services.Shared.Dtos;

// namespace Application.Services.Shared.DtoValidation
// {
//     public class ContactValidator : AbstractValidator<ContactDto>
//     {
//         public ContactValidator()
//         {

//             RuleFor(xx => xx.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(150);
//             RuleFor(xx => xx.Site).MaximumLength(150);

//             When(xx => string.IsNullOrEmpty(xx.Cel) && string.IsNullOrEmpty(xx.Zap) && string.IsNullOrEmpty(xx.Landline), () =>
//             {
//                 RuleFor(xx => xx.Zap).NotNull().NotEmpty().MaximumLength(11);
//                 RuleFor(xx => xx.Cel).NotNull().NotEmpty().MaximumLength(11);
//                 RuleFor(xx => xx.Landline).NotNull().NotEmpty().MaximumLength(10);

//             }).Otherwise(() =>
//             {
//                 RuleFor(xx => xx.Zap).MaximumLength(11);
//                 RuleFor(xx => xx.Cel).MaximumLength(11);
//                 RuleFor(xx => xx.Landline).MaximumLength(10);
//             });

//             RuleForEach(xx => xx.SocialMedias).SetValidator(new SocialnetworksValidator());

//         }
//     }
// }