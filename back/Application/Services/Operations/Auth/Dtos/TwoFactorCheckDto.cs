using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Account.dtos;

public class TwoFactorCheckDto
{
    public required string Token { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}