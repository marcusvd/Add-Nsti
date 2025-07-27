using Application.Services.Operations.Companies.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Authentication.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}