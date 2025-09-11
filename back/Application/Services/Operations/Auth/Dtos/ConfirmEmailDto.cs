
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Dtos;

public class ConfirmEmailDto
{
        [Required]
        public string Token { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}