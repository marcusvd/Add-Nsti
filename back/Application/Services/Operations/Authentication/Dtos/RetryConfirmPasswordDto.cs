using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class RetryConfirmPasswordDto
    {
        // [Required]
        // public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}