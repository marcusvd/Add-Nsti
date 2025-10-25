
using System.ComponentModel.DataAnnotations;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;

public class EmailConfirmManualDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
}