using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.Authentication;
using Domain.Entities.Shared;
using Domain.Entities.System.BusinessesCompanies;
using System.Linq;

namespace Application.Services.Operations.Profiles.Dtos;

public class BusinessProfileDto : RootBase
{
    public required string BusinessAuthId { get; set; }

    public ICollection<UserProfileDto> UsersAccounts { get; set; } = new List<UserProfileDto>();
    public ICollection<CompanyProfileDto> Companies { get; set; } = new List<CompanyProfileDto>();
}

public static class BusinessProfileMapper
{
    public static BusinessProfile ToEntity(this BusinessProfileDto businessProfileDto)
    {

        BusinessProfile businessProfile = new()
        {
            Id = businessProfileDto.Id,
            BusinessAuthId = businessProfileDto.BusinessAuthId,
            UsersAccounts = businessProfileDto.UsersAccounts.Select(x => x.ToEntity()).ToList(),
            Companies = businessProfileDto.Companies.Select(x => x.ToEntity()).ToList()
        };

        return businessProfile;
    }

    public static BusinessProfileDto ToDto(this BusinessProfile businessProfile)
    {
        BusinessProfileDto businessProfileEntity = new()
        {
            Id = businessProfile.Id,
            BusinessAuthId = businessProfile.BusinessAuthId,
            UsersAccounts = businessProfile.UsersAccounts.Select(x => x.ToDto()).ToList(),
            Companies = businessProfile.Companies.Select(x => x.ToDto()).ToList()
        };

        return businessProfileEntity;
    }

    public static BusinessProfileDto Incomplete()
    {
         BusinessProfileDto businessProfileEntity = new()
        {
            Id = 0,
            BusinessAuthId = "Cadastro Incompleto",
            UsersAccounts = [],
            Companies = []
        };

        return businessProfileEntity;
    }


}