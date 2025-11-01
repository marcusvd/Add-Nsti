using Application.Businesses.Dtos;
using Application.CompaniesServices.Dtos.Extends;
using Application.Shared.Dtos;
using Domain.Entities.System.Companies;

namespace Application.CompaniesServices.Dtos.Profile;

public class CompanyProfileDto : CompanyBaseDto
{
    public int BusinessProfileId { get; set; }
    public BusinessProfileDto? BusinessProfile { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }

    public static implicit operator CompanyProfile(CompanyProfileDto dto)
    {
        return new CompanyProfile
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            BusinessProfileId = dto.BusinessProfileId,
            CNPJ = dto.CNPJ,
            Address = dto.Address,
            Contact = dto.Contact,
        };

    }
    public static implicit operator CompanyProfileDto(CompanyProfile dto)
    {
        return new CompanyProfileDto
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            BusinessProfileId = dto.BusinessProfileId,
            CNPJ = dto.CNPJ,
            Address = dto.Address,
            Contact = dto.Contact,
        };

    }

}