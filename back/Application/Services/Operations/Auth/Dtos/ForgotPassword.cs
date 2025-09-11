
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Dtos;

public class ForgotPasswordDto
{
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}