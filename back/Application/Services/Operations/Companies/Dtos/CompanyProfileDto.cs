
using Application.Services.Shared.Dtos;
using Domain.Entities.System.BusinessesCompanies;

namespace Application.Services.Operations.Companies.Dtos;

public class CompanyProfileDto : RootBaseDto
{
    public int BusinessProfileId { get; set; }
    public required string CompanyAuthId { get; set; }
    public required string CNPJ { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
}


public static class DtoMapper
{

    public static CompanyProfile ToEntity(this CompanyProfileDto companyProfileDto)
    {
        if (companyProfileDto.Address is null)
            companyProfileDto.Address = AddressMapper.Incomplete();

        if (companyProfileDto.Contact is null)
            companyProfileDto.Contact = ContactMapper.Incomplete();


        CompanyProfile companyProfile = new()
        {
            Id = companyProfileDto.Id,
            Deleted = companyProfileDto.Deleted,
            Registered = companyProfileDto.Registered,
            BusinessProfileId = companyProfileDto.BusinessProfileId,
            CompanyAuthId = companyProfileDto.CompanyAuthId,
            CNPJ = companyProfileDto.CNPJ,
            Address = companyProfileDto.Address.ToEntity(),
            Contact = companyProfileDto.Contact.ToEntity(),

        };
        return companyProfile;
    }
    public static CompanyProfileDto ToDto(this CompanyProfile companyProfile)
    {
        if (companyProfile.Address is null)
            companyProfile.Address = AddressMapper.Incomplete().ToEntity();

        if (companyProfile.Contact is null)
            companyProfile.Contact = ContactMapper.Incomplete().ToEntity();


        CompanyProfileDto companyProfileDto = new()
        {
            Id = companyProfile.Id,
            Deleted = companyProfile.Deleted,
            Registered = companyProfile.Registered,
            BusinessProfileId = companyProfile.BusinessProfileId,
            CompanyAuthId = companyProfile.CompanyAuthId,
            CNPJ = companyProfile.CNPJ,
            Address = companyProfile.Address.ToDto(),
            Contact = companyProfile.Contact.ToDto(),

        };
        return companyProfileDto;
    }

}
