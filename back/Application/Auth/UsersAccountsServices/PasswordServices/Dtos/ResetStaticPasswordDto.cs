using System.ComponentModel.DataAnnotations;

namespace AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;

public class ResetStaticPasswordDto : IValidatableObject
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        var resultEmpty = IsPasswordOrEmailEmpty(validationContext);
        var resultMatch = IsPasswordMatch(validationContext);

        result.AddRange(resultEmpty);
        result.AddRange(resultMatch);

        return result;
    }

    private IEnumerable<ValidationResult> IsPasswordOrEmailEmpty(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            yield return new ValidationResult("Email ou senha inválido.", new[] { nameof(Password), nameof(Email) });

        Validated();
    }
    private IEnumerable<ValidationResult> IsPasswordMatch(ValidationContext validationContext)
    {
        if (Password != ConfirmPassword)
            yield return new ValidationResult("As senhas não correspondem.", new[] { nameof(Password), nameof(ConfirmPassword) });

        Validated();
    }

    private IEnumerable<ValidationResult> Validated()
    {
        yield return new ValidationResult("");
    }



}