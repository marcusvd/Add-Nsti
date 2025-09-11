
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Dtos;

public class AccountLockedOutManualDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public bool AccountLockedOut { get; set; }
}