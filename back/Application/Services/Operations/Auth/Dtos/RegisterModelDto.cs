

using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class RegisterModelDto
{
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string CompanyName { get; set; }
    public required string CNPJ { get; set; }
    public required string Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public AddressDto? Address { get; set; } = AddressMapper.Incomplete();
    public ContactDto? Contact { get; set; } = ContactMapper.Incomplete();
}
