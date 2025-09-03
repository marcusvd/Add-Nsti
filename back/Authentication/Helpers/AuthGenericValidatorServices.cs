using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;


namespace Authentication.Helpers;

public class AuthGenericValidatorServices
{
    private readonly ILogger<AuthGenericValidatorServices> _logger;

    public AuthGenericValidatorServices(ILogger<AuthGenericValidatorServices> logger)
    {
        _logger = logger;
    }

    public void IsObjNull<T>(T obj) where T : class
    {
        var validator = new IsObjNull<T>();
        var result = validator.Validate(obj);

        if (!result.IsValid)
        {
            _logger.LogError("Validation failed for {type}: {Errors}",
            typeof(T).Name, string.Join(", ", result.Errors));
            throw new ArgumentException(string.Join(", ", result.Errors));
        }

    }


    public bool Validate<T>(T dtoId, T paramId, string messageException)
    {
        if (!Equals(dtoId, paramId)) throw new AuthServicesException(messageException);
        else
            return true;
    }


    public Object ReplaceNullObj<T>()
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




}

