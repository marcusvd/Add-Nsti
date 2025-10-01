
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Dtos;

public class ResendConfirmEmailViewModel
{
        [Required]

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}