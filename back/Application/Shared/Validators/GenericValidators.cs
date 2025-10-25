using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Authentication.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;


namespace Application.Shared.Validators;

public class GenericValidators : IGenericValidators
{
    public GenericValidators(
        // ILogger logger
        // ILogger<GenericValidatorServices> logger
        )
    {
        // _logger = logger;
    }

    public void IsObjNull<T>(T obj) where T : class
    {
        var validator = new IsObjNull<T>();
        var result = validator.Validate(obj);

        if (!result.IsValid)
        {
            // _logger.LogError("Validation failed for {type}: {Errors}", typeof(T).Name, string.Join(", ", result.Errors));
            throw new ArgumentException(string.Join(", ", result.Errors));
        }

    }

    public List<T> EmptyListBuilder<T>(List<T> obj) where T : class => obj.Any() ? obj : new List<T>();

    public bool Validate<T>(T dtoId, T paramId, string messageException)
    {
        if (!Equals(dtoId, paramId)) throw new AuthServicesException(messageException);
        else
            return true;
    }
    public static Object ReplaceNullObj<T>()
    {
        var type = typeof(T);

        var instance = Activator.CreateInstance(type);

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            bool isRequired = Attribute.IsDefined(prop, typeof(SetsRequiredMembersAttribute));
            if (isRequired || !prop.CanWrite) continue;

            object? value = prop.PropertyType switch
            {
                Type t when t == typeof(string) => "Cadastro Incompleto",
                Type t when t == typeof(int) => 0,
                _ => null
            };

            if (value != null)
                prop.SetValue(instance, value);
        }

        return instance ?? new Object();

    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


}

public class IsObjNull<T> : AbstractValidator<T> where T : class
{
    public IsObjNull()
    {
        RuleFor(x => x).NotNull()
        .WithMessage(AuthErrorsMessagesException.ObjectIsNull)
        .WithName(typeof(T).Name);
    }

}