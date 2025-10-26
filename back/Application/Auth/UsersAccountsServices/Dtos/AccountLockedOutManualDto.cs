
using System.ComponentModel.DataAnnotations;

namespace Application.UsersAccountsServices.Dtos;

public class AccountLockedOutManualDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public bool AccountLockedOut { get; set; }
}