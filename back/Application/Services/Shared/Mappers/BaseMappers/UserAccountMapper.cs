using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;
using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;

namespace Application.Services.Shared.Mappers.BaseMappers;

public class UserAccountEntityMapper : BaseMapper<UserAccount, UserAccountDto>
{
    public override UserAccountDto Map(UserAccount source)
    {
        if (source == null) return new UserAccountDto() { UserProfileId = "invalid", DisplayUserName = "invalid", Email = "invalid" };

        var destination = base.Map(source);

        return destination;
    }
}
public class UserAccountDtoMapper : BaseMapper<UserAccountDto, UserAccount>
{
    public override UserAccount Map(UserAccountDto source)
    {
        if (source == null) return new UserAccount() { UserProfileId = "invalid", DisplayUserName = "invalid", Email = "invalid" };

        var destination = base.Map(source);

        return destination;
    }
}

public class UserProfileEntityMapper : BaseMapper<UserProfile, UserProfileDto>
{
    private readonly IMapper<Address, AddressDto> _addressMapper;
    private readonly IMapper<Contact, ContactDto> _contactMapper;
    public UserProfileEntityMapper(
            IMapper<Address, AddressDto> addressMapper,
            IMapper<Contact, ContactDto> contactMapper
            )
    {
        _addressMapper = addressMapper;
        _contactMapper = contactMapper;
    }

    public override UserProfileDto Map(UserProfile source)
    {
        if (source == null) return new UserProfileDto() { UserAccountId = "invalid" };

        var destination = base.Map(source);

        destination.Address = _addressMapper.Map(source.Address ?? new Address());
        destination.Contact = _contactMapper.Map(source.Contact ?? new Contact());

        return destination;
    }
}
public class UserProfileDtoMapper : BaseMapper<UserProfileDto, UserProfile>
{
    private readonly IMapper<AddressDto, Address> _addressMapper;
    private readonly IMapper<ContactDto, Contact> _contactMapper;
    public UserProfileDtoMapper(
            IMapper<AddressDto, Address> addressMapper,
            IMapper<ContactDto, Contact> contactMapper
            )
    {
        _addressMapper = addressMapper;
        _contactMapper = contactMapper;
    }

    public override UserProfile Map(UserProfileDto source)
    {
        if (source == null) return new UserProfile() { UserAccountId = "invalid" };

        var destination = base.Map(source);

        destination.Address = _addressMapper.Map(source.Address ?? new AddressDto());
        destination.Contact = _contactMapper.Map(source.Contact ?? new ContactDto());

        return destination;
    }
}