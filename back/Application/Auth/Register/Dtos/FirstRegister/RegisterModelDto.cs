using Application.Auth.Register.Dtos.Extends;

namespace Application.Auth.Register.Dtos.FirstRegister;

public class RegisterModelDto : RegisterBaseDto
{
    public required string CompanyName { get; set; }
    public required string CNPJ { get; set; }
    public string? ConfirmPassword { get; set; }
}


