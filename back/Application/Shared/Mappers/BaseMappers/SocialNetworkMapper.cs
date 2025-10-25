// using Application.Shared.Dtos;
// using Domain.Entities.Shared;

// namespace Application.Shared.Mappers.BaseMappers;

// public class SocialNetworkMapper : BaseMapper<SocialNetwork, SocialNetworkDto>
// {
//     public override SocialNetworkDto Map(SocialNetwork source)
//     {
//         if (source == null) return new SocialNetworkDto() { Name = "invalid", Url = "invalid" };

//         var destination = base.Map(source);

//         return destination;
//     }
// }