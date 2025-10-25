
namespace Application.CompaniesServices.Dtos.Extends;

public class CompanyBaseDto : ICompanyBaseDto
{
    public int Id { get; set; }
    public required string CNPJ { get; set; }
    public DateTime Deleted { get; set; } = DateTime.MinValue;
    public DateTime Registered { get; set; } = DateTime.UtcNow;
}

// public static class DtoMapper
// {

//     public static CompanyProfile ToEntity(this CompanyProfileDto companyProfileDto)
//     {
//         if (companyProfileDto.Address is null)
//             companyProfileDto.Address = AddressMapper.Incomplete();

//         if (companyProfileDto.Contact is null)
//             companyProfileDto.Contact = ContactMapper.Incomplete();


//         CompanyProfile companyProfile = new()
//         {
//             Id = companyProfileDto.Id,
//             Deleted = companyProfileDto.Deleted,
//             Registered = companyProfileDto.Registered,
//             BusinessProfileId = companyProfileDto.BusinessProfileId,
//             // CompanyAuthId = companyProfileDto.CompanyAuthId,
//             CNPJ = companyProfileDto.CNPJ,
//             Address = companyProfileDto.Address,
//             Contact = companyProfileDto.Contact,
//         };
//         return companyProfile;
//     }
//     public static CompanyProfileDto ToDto(this CompanyProfile companyProfile)
//     {
        
//         if (companyProfile.Address is null)
//             companyProfile.Address = AddressMapper.Incomplete().ToEntity();

//         if (companyProfile.Contact is null)
//             companyProfile.Contact = ContactMapper.Incomplete().ToEntity();


//         return new()
//         {
//             Id = companyProfile.Id,
//             Deleted = companyProfile.Deleted,
//             Registered = companyProfile.Registered,
//             BusinessProfileId = companyProfile.BusinessProfileId,
//             CNPJ = companyProfile.CNPJ,
//             Address = companyProfile.Address,
//             Contact = companyProfile.Contact,

//         };

//     }

//     public static CompanyProfile ToUpdateSimple(this CompanyProfileDto dto, CompanyProfile entity)
//     {
//         entity.CNPJ = dto.CNPJ;
//         entity.BusinessProfileId = dto.BusinessProfileId;
//         entity.BusinessProfile = dto.BusinessProfile;
//         entity.Address = dto.Address?.ToEntity();
//         entity.Contact = dto.Contact?.ToEntity();
//         return entity;
//     }

// }
