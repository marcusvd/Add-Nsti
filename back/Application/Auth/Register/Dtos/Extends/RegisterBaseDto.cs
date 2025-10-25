using Application.Auth.Register.Dtos;
using Application.Auth.Register.Dtos.FirstRegister;
using Application.Auth.UsersAccountsServices.Dtos;
using Application.Businesses.Dtos;
using Application.CompaniesServices.Dtos.Profile;
using Application.Shared.Dtos;
using Application.UsersAccountsServices.Dtos;
using Domain.Entities.Authentication;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Companies;

namespace Application.Auth.Register.Dtos.Extends;

public abstract class RegisterBaseDto : IRegisterBaseDto
{
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public AddressDto? Address { get; set; } = new();
    public ContactDto? Contact { get; set; } = new();
    public static UserAccount CreateUserAccount(RegisterModelDto user, BusinessAuth business, string userProfileId)
    {
        return new UserAccount()
        {
            DisplayUserName = user.UserName,
            UserName = user.Email,
            Email = user.Email,
            UserProfileId = userProfileId,
            BusinessAuth = business
        };
    }
    public static UserAccountDto CreateUserAccount(AddUserExistingCompanyDto user, int businessAuthId, string userProfileId)
    {
        var userAccount = new UserAccountDto()
        {
            
            DisplayUserName = user.UserName,
            UserName = user.Email,
            Email = user.Email,
            UserProfileId = userProfileId,
            BusinessAuthId = businessAuthId
        };
        return userAccount;
    }
    public static UserProfileDto CreateUserProfile(string userAccountId, ContactDto? contact, AddressDto? address, int businessProfileId = -1)
    {
        var userProfileDto = new UserProfileDto()
        {
            Id = 0,
            UserAccountId = userAccountId,
            BusinessProfileId = businessProfileId,
            Address = address,
            Contact = contact
        };
        return userProfileDto;
    }
    public static CompanyAuth CreateCompanyAuth(string name, string CNPJ)
    {

        return new CompanyAuth()
        {
            Id = 0,
            Name = name,
            TradeName = name,
            CNPJ = CNPJ
        };
    }
    public static CompanyProfileDto CreateCompanyProfile(RegisterModelDto user)
    {
        if (user.Address is null)
            user.Address = new();

        if (user.Contact is null)
            user.Contact = new();

        return new CompanyProfileDto()
        {
            Id = 0,
            CNPJ = user.CNPJ,
            Address = user.Address,
            Contact = user.Contact
        };

    }
    public static BusinessAuth CreateBusinessAuth(CompanyAuth company, string BusinessProfileId)
    {
        return new BusinessAuth()
        {
            Id = 0,
            Name = $"Grupo empresarial",
            BusinessProfileId = BusinessProfileId,
            Companies = new List<CompanyAuth>() { company },
        };
    }
    public static BusinessProfileDto CreateBusinessProfile(string businessProfileId, CompanyProfileDto company, UserProfileDto user)
    {
        return new BusinessProfileDto()
        {
            Id = 0,
            BusinessAuthId = businessProfileId,
            UsersAccounts = new List<UserProfileDto>() { user },
            Companies = new List<CompanyProfileDto>() { company }
        };
    }
}
