
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Dtos;

public class EmailConfirmManualDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}