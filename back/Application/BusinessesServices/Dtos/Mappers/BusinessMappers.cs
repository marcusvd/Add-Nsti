
namespace Application.Businesses.Dtos.Mappers;

public static class BusinessMappers
{
    // public static BusinessBaseDb ToMapper(this BusinessBaseDto dto) => dto switch
    // {
    //     BusinessAuthDto auth => new BusinessAuth()
    //     {
    //         Id = auth.Id,
    //         Deleted = auth.Deleted,
    //         Registered = auth.Registered,
    //         Name = auth.Name,
    //         BusinessProfileId = auth.BusinessProfileId
    //     },

    //     BusinessProfileDto profile => new BusinessProfile()
    //     {
    //         Id = profile.Id,
    //         Deleted = profile.Deleted,
    //         Registered = profile.Registered,
    //         BusinessAuthId = profile.BusinessAuthId
    //     },
    //     _ =>
    //     throw new BusinessException("Mapping failed.")
    // };
    // public static BusinessBaseDto ToMapper(this BusinessBaseDb dto) => dto switch
    // {
    //     BusinessAuth auth => new BusinessAuthDto()
    //     {
    //         Id = auth.Id,
    //         Deleted = auth.Deleted,
    //         Registered = auth.Registered,
    //         Name = auth.Name,
    //         BusinessProfileId = auth.BusinessProfileId
    //     },

    //     BusinessProfile profile => new BusinessProfileDto()
    //     {
    //         Id = profile.Id,
    //         Deleted = profile.Deleted,
    //         Registered = profile.Registered,
    //         BusinessAuthId = profile.BusinessAuthId
    //     },
    //     _ =>
    //     throw new BusinessException("Mapping failed.")
    // };
    // public static List<TDb> ToMapperListDb<TDto, TDb>(this List<TDto> listDto) where TDb : BusinessBaseDb where TDto : BusinessBaseDto
    // {
    //     return listDto.Select(x => (TDb)x.ToMapper()).ToList();
    // }
    // public static List<TDto> ToMapperListDto<TDb, TDto>(this List<TDb> listDb) where TDto : BusinessBaseDto where TDb : BusinessBaseDb
    // {
    //     return listDb.Select(x => (TDto)x.ToMapper()).ToList();
    // }

}