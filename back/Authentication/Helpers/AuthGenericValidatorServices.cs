using System;
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

}

