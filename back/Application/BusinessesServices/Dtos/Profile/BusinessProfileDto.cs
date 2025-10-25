using Application.Auth.UsersAccountsServices.Dtos;
using Domain.Entities.System.Businesses;
using Domain.Entities.System.Profiles;
using Domain.Entities.System.Companies;
using Application.CompaniesServices.Dtos.Profile;
using Application.BusinessesServices.Extends;


namespace Application.Businesses.Dtos;

public class BusinessProfileDto : BusinessBaseDto
{
    public required string BusinessAuthId { get; set; }
    public ICollection<UserProfileDto> UsersAccounts { get; set; } = new List<UserProfileDto>();
    public ICollection<CompanyProfileDto> Companies { get; set; } = new List<CompanyProfileDto>();

    public static implicit operator BusinessProfileDto(BusinessProfile map)
    {
        return new()
        {
            Id = map.Id,
            BusinessAuthId = map.BusinessAuthId,
            UsersAccounts = map.UsersAccounts.Select(x => (UserProfileDto)x).ToList(),
            Companies = map.Companies.Select(x=> (CompanyProfileDto)x).ToList()
        };
    }
    public static implicit operator BusinessProfile(BusinessProfileDto map)
    {
        return new()
        {
            Id = map.Id,
            BusinessAuthId = map.BusinessAuthId,
            UsersAccounts = map.UsersAccounts.Select(x => (UserProfile)x).ToList(),
            Companies = map.Companies.Select(x => (CompanyProfile)x).ToList()
        };
    }

}




// public static class DtoMapper
// {


//     public static BusinessProfile ToUpdateSimple(this BusinessProfileDto dto, BusinessProfile entity)
//     {
//         entity.CNPJ = dto.CNPJ;
//         entity.BusinessProfileId = dto.BusinessProfileId;
//         entity.BusinessProfile = dto.BusinessProfile;
//         entity.Address = dto.Address?.ToEntity();
//         entity.Contact = dto.Contact?.ToEntity();
//         return entity;
//     }

// }
