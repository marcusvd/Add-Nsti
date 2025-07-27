
using Application.Exceptions;
using FluentValidation;

namespace Application.Services.Helpers;

public  class IsObjNull<T> : AbstractValidator<T> where T : class
{
    public IsObjNull()
    {
        RuleFor(x => x).NotNull()
        .WithMessage(GlobalErrorsMessagesException.IsObjNull)
        .WithName(typeof(T).Name);
    }

}

