using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class ForgotPasswordDto
    {
        // [Required]
        // public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}