
using System.ComponentModel.DataAnnotations;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;

public class ResendConfirmEmailViewModel
{
        [Required]

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}