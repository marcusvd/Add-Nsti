using Microsoft.AspNetCore.Identity;

namespace Application.Shared.Dtos;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];

    public static ApiResponse<T> Response(List<string> errors, bool result, string message, T obj)
    {
        var errorsListFormat = new List<string>();

        var response = new ApiResponse<T>
        {
            Success = result,
            Message = message,
            Data = obj,
            Errors = new List<string>()
        };

        if (!result)
        {
            if (errors.Any())
            {
                errors.ForEach(x =>
                {
                    errorsListFormat.Add(x);

                });
            }

            var identityResult = IdentityResultsHandler(obj);

            if (identityResult.Any())
                identityResult.ForEach(x => errorsListFormat.Add(x));

            response.Errors = errorsListFormat;
        }

        return response;

    }
    private static List<string> IdentityResultsHandler(T obj)
    {
        var errorsList = new List<string>();

        if (obj is IdentityResult ir)
            if (!ir.Succeeded)
                ir.Errors.ToList().ForEach(x => errorsList.Add(@$"{x.Code}-{x.Description}"));

        return errorsList;
    }


}