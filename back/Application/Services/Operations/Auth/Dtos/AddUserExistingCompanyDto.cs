using Application.Services.Shared.Dtos;

namespace Application.Services.Operations.Auth.Dtos;

public class AddUserExistingCompanyDto
{
    public int companyAuthId { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }

}