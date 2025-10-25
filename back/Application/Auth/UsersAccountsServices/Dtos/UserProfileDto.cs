using Application.Auth.UsersAccountsServices.Dtos.Extends;
using Application.Businesses.Dtos;
using Application.Shared.Dtos;
using Domain.Entities.System.Profiles;

namespace Application.Auth.UsersAccountsServices.Dtos;

public class UserProfileDto : UserAccountBaseDto
{
    public required string UserAccountId { get; set; }
    public int BusinessProfileId { get; set; }
    public AddressDto? Address { get; set; }
    public ContactDto? Contact { get; set; }
    public BusinessProfileDto? BusinessProfile { get; set; }

    public static implicit operator UserProfile(UserProfileDto dto)
    {
        return new UserProfile
        {
            BusinessProfileId = dto.BusinessProfileId,
            BusinessProfile = dto.BusinessProfile,
            UserAccountId = dto.UserAccountId,
            Address = dto.Address,
            Contact = dto.Contact,
        };
    }

    public static implicit operator UserProfileDto(UserProfile dto)
    {
        return new UserProfileDto
        {
            BusinessProfileId = dto.BusinessProfileId,
            BusinessProfile = dto.BusinessProfile,
            UserAccountId = dto.UserAccountId,
            Address = dto.Address,
            Contact = dto.Contact,
        };
    }

}