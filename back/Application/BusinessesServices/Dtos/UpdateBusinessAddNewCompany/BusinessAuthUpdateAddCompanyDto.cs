using Application.CompaniesServices.Dtos.Auth;
using Application.Shared.Dtos;

namespace Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;

public class BusinessAuthUpdateAddCompanyDto 
{
    public int Id { get; set; }
    public required string BusinessProfileId { get; set; }
    public required string CNPJ { get; set; }
    public CompanyAuthDto Company { get; set; } = new() {  Name = "invalid", TradeName = "invalid", CNPJ = "invalid"};
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
}
public class PushCompanyDto 
{
    public int BusinessId { get; set; }
    public required string BusinessProfileId { get; set; }
    public required string CNPJ { get; set; }
    public required string Role { get; set; } = "HOLDER";
    public CompanyAuthDto Company { get; set; } = new() { CNPJ = "invalid", Name = "invalid", TradeName = "invalid" };
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
}