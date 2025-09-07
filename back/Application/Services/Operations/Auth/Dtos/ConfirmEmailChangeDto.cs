using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Account.dtos;

public class ConfirmEmailChangeDto
{
    public required int Id { get; set; }
    public required string Token { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}