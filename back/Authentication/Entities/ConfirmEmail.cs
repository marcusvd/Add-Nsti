
using System.ComponentModel.DataAnnotations;

namespace Authentication.Entities;

public class ConfirmEmail
{
        [Required]
        public string Token { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}