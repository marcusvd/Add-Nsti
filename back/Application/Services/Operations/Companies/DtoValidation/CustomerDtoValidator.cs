using FluentValidation;
using Application.Services.Operations.Companies.Dtos;

namespace Application.Services.Operations.Companies.DtoValidation
{
    public class CompanyDtoValidator : AbstractValidator<CompanyProfileDto>
    {
        public CompanyDtoValidator()
        {
           
        }
    }
}


