// using FluentValidation;
// using Application.Services.Shared.Dtos;

// namespace Application.Services.Shared.DtoValidation
// {
//     public class SocialnetworksValidator : AbstractValidator<SocialNetworkDto>
//     {
//         public SocialnetworksValidator()
//         {
//             RuleFor(xx => xx.Name).NotNull().NotEmpty().MaximumLength(150);
//             RuleFor(xx => xx.Url).NotNull().NotEmpty().MaximumLength(150);
//         }
//     }
// }