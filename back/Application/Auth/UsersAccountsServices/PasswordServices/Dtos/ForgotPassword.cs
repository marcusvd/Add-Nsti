
using System.ComponentModel.DataAnnotations;

namespace AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;

public class ForgotPasswordDto
{
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}