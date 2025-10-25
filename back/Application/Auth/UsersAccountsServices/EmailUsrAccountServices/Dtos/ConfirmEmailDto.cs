
using System.ComponentModel.DataAnnotations;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;

public class ConfirmEmailDto
{
        [Required]
        public string Token { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}