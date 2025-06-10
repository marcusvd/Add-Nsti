using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class ConfirmEmailDto
    {
        [Required]
        public string Token { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}