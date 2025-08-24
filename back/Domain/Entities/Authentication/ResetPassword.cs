using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Authentication;

public class ResetPassword
{
    [Required]
    public string Token { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }

     public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Password != ConfirmPassword)
        {
            yield return new ValidationResult(
                "Passwords do not match.",
                new[] { nameof(Password), nameof(ConfirmPassword) }
            );
        }
    }


}