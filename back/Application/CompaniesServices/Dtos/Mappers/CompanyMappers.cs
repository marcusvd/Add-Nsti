using Application.CompaniesServices.Dtos.Auth;
using Domain.Entities.System.Companies;
using Application.CompaniesServices.Dtos.Profile;

namespace Application.CompaniesServices.Dtos.Mappers;

public static class CompanyMappers
{
    public static CompanyProfile ToUpdateSimple(this CompanyProfile db, CompanyProfileDto dto)
    {
        db.CNPJ = dto.CNPJ;
        db.BusinessProfileId = dto.BusinessProfileId;
        db.Address = dto.Address;
        db.Contact = dto.Contact;
        return db;
    }
    public static CompanyAuth ToUpdateSimple(this CompanyAuth db, CompanyAuthDto dto)
    {
        db.CNPJ = dto.CNPJ;
        db.Name = dto.Name;
        db.TradeName = dto.TradeName;
        return db;
    }


    // public static CompanyBaseDb ToMapper(this CompanyBaseDto dto) => dto switch
    // {
    //     CompanyAuthDto auth => new CompanyAuth
    //     {
    //         Id = auth.Id,
    //         Deleted = auth.Deleted,
    //         Registered = auth.Registered,
    //         CNPJ = auth.CNPJ,
    //         Name = auth.Name,
    //         TradeName = auth.TradeName
    //     },

    //     CompanyProfileDto profile => new CompanyProfile
    //     {
    //         Id = profile.Id,
    //         Deleted = profile.Deleted,
    //         Registered = profile.Registered,
    //         BusinessProfileId = profile.BusinessProfileId,
    //         CNPJ = profile.CNPJ,
    //         Address = profile.Address!.ToEntity(),
    //         Contact = profile.Contact!.ToEntity(),
    //     },
    //     _ => throw new CompanyException("Mapping failed.")
    // };
    // public static CompanyBaseDto ToMapper(this CompanyBaseDb db) => db switch
    // {

    //     CompanyAuth authDb => new CompanyAuthDto
    //     {
    //         Id = authDb.Id,
    //         Deleted = authDb.Deleted,
    //         Registered = authDb.Registered,
    //         CNPJ = authDb.CNPJ,
    //         Name = authDb.Name,
    //         TradeName = authDb.TradeName
    //     },

    //     CompanyProfile profileDb => new CompanyProfileDto
    //     {
    //         Id = profileDb.Id,
    //         Deleted = profileDb.Deleted,
    //         Registered = profileDb.Registered,
    //         BusinessProfileId = profileDb.BusinessProfileId,
    //         CNPJ = profileDb.CNPJ,
    //         Address = profileDb.Address!.ToDto(),
    //         Contact = profileDb.Contact!.ToDto(),
    //     },
    //     _ => throw new CompanyException("Mapping failed.")
    // };

    // public static List<TDb> ToMapperListDb<TDb, TDto>(this List<TDto> listDto) where TDb : CompanyBaseDb where TDto : CompanyBaseDto
    // {
    //     return listDto.Select(x => (TDb)x.ToMapper()).ToList();
    // }
    // public static List<TDto> ToMapperListDTo<TDto, TDb>(this List<TDb> listDto) where TDto : CompanyBaseDto where TDb : CompanyBaseDb
    // {
    //     return listDto.Select(x => (TDto)x.ToMapper()).ToList();
    // }




    //  public static List<CompanyProfile> ToMapperList(this List<CompanyProfileDto> listDto)
    //     {
    //         return listDto.Select(x => (CompanyProfile)x.ToMapper()).ToList();
    //     }
    //     public static List<CompanyProfileDto> ToMapperList(this List<CompanyProfile> listDb)
    //     {
    //         return listDb.Select(x => (CompanyProfileDto)x.ToMapper()).ToList();
    //     }
    //     public static List<CompanyAuth> ToMapperList(this List<CompanyAuthDto> listDto)
    //     {
    //         return listDto.Select(x => (CompanyAuth)x.ToMapper()).ToList();
    //     }
    //     public static List<CompanyAuthDto> ToMapperList(this List<CompanyAuth> listDb)
    //     {
    //         return listDb.Select(x => (CompanyAuthDto)x.ToMapper()).ToList();
    //     }





}