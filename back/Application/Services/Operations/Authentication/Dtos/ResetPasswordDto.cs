using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        
        //public string UserName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}