using Application.CompaniesServices.Dtos.Extends;
using Application.Shared.Validators;
using Application.Auth.Dtos;
using Domain.Entities.System.Companies;
using Application.BusinessesServices.Dtos.Auth;
using Domain.Entities.Authentication;

namespace Application.CompaniesServices.Dtos.Auth;

public class CompanyAuthDto : CompanyBaseDto
{
    public int BusinessAuthId { get; set; }
    public required string Name { get; set; }
    public required string TradeName { get; set; }
    public BusinessAuthDto Business { get; set; } = (BusinessAuthDto)GenericValidators.ReplaceNullObj<BusinessAuthDto>();
    public ICollection<CompanyUserAccountDto> CompanyUserAccounts { get; set; } = new List<CompanyUserAccountDto>();

    public static implicit operator CompanyAuth(CompanyAuthDto dto)
    {
        return new CompanyAuth
        {
            Id = dto.Id,
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            CNPJ = dto.CNPJ,
            Name = dto.Name,
            TradeName = dto.TradeName,
            CompanyUserAccounts = dto.CompanyUserAccounts.Select(x=> (CompanyUserAccount)x).ToList()
        };

    }
    public static implicit operator CompanyAuthDto(CompanyAuth db)
    {
        return new CompanyAuthDto
        {
            Id = db.Id,
            Deleted = db.Deleted,
            Registered = db.Registered,
            CNPJ = db.CNPJ,
            Name = db.Name,
            TradeName = db.TradeName,
            
            CompanyUserAccounts = db.CompanyUserAccounts.Select(x=> (CompanyUserAccountDto)x).ToList()
        };

    }

}

