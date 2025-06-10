using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class T2FactorDto
    {
        // [Required]
        public string UserName { get; set; }
        [Required]
        public string Token { get; set; }
    }
}