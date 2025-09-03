using Application.Services.Operations.Auth.Dtos;
using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Services.Operations.Auth.Dtos;

public class CompanyAuthDto : RootBaseDto
{
    public required string CompanyProfileId { get; set; }
    public required string Name { get; set; }
    public required string TradeName { get; set; }
    public int BusinessId { get; set; }
    public BusinessAuthDto Business { get; set; } = BusinessAuthMapper.Incomplete();
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();
}

public static class CompanyAuthMapper
{
    public static CompanyAuth ToEntity(this CompanyAuthDto dto)
    {
        CompanyAuth companyAuth = new()
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            CompanyProfileId = dto.CompanyProfileId,
            Name = dto.Name,
            TradeName = dto.TradeName,
            BusinessId = dto.BusinessId,            
            CompanyUserAccounts = dto.CompanyUserAccounts.Select(x => x.ToEntity()).ToList()

        };

        return companyAuth;
    }

    public static CompanyAuthDto ToDto(this CompanyAuth entity)
    {
        CompanyAuthDto companyAuth = new()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            CompanyProfileId = entity.CompanyProfileId,
            Name = entity.Name,
            TradeName = entity.TradeName,
            BusinessId = entity.BusinessId,            
            CompanyUserAccounts = entity.CompanyUserAccounts.Select(x => x.ToDto()).ToList()

        };
        return companyAuth;
    }

    public static CompanyAuthDto Incomplete()
    {
        CompanyAuthDto incomplete = new()
        {
            Id = 0,
            Deleted = DateTime.MinValue,
            Registered = DateTime.Now,
            CompanyProfileId = "Cadastro Incompleto",
            Name = "Cadastro Incompleto",
            TradeName = "Cadastro Incompleto",
            BusinessId = 0,
        };
      
        return incomplete;
    }


}