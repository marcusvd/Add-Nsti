using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Dtos;

public class BusinessAuthDto : RootBaseDto
{

    public required string Name { get; set; }
    public required string BusinessProfileId { get; set; }
    public ICollection<UserAccountDto> UsersAccounts { get; set; } = new List<UserAccountDto>();
    public ICollection<CompanyAuthDto> Companies { get; set; } = new List<CompanyAuthDto>();
}

public static class BusinessAuthMapper
{
    public static BusinessAuth ToEntity(this BusinessAuthDto dto)
    {
        BusinessAuth address = new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            Name = dto.Name,
            BusinessProfileId = dto.BusinessProfileId,
            UsersAccounts = dto.UsersAccounts.Select(x => x.ToEntity()).ToList(),
            Companies = dto.Companies.Select(x => x.ToEntity()).ToList()

        };

        return address;
    }

    public static BusinessAuthDto ToDto(this BusinessAuth entity)
    {
        BusinessAuthDto businessAuth = new()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            Name = entity.Name,
            BusinessProfileId = entity.BusinessProfileId,
            UsersAccounts = entity.UsersAccounts.Select(x => x.ToDto()).ToList(),
            Companies = entity.Companies.Select(x => x.ToDto()).ToList()

        };
        return businessAuth;
    }

    public static BusinessAuthDto Incomplete()
    {
        BusinessAuthDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
            Name = "Cadastro Incompleto",
            BusinessProfileId = "Cadastro Incompleto",
            UsersAccounts = new List<UserAccountDto>(),
            Companies = new List<CompanyAuthDto>()

        };
        // incomplete.UsersAccounts.Add(UserAccountMapper.Incomplete());
        // incomplete.Companies.Add(CompanyAuthMapper.Incomplete());

        return incomplete;
    }


}