using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;
using Application.Services.Operations.Profiles.Dtos;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Authentication.Exceptions;
using Application.Exceptions;

namespace Application.Services.Shared.Dtos.Mappers;

public class CommonObjectMapper : ICommonObjectMapper
{
    public BusinessAuth BusinessAuthMapperUpdate(BusinessAuth db, BusinessAuthUpdateAddCompanyDto dto)
    {

        if(dto.Company == null) throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);

        // db.Name = dto.Name;
        // db.Deleted = dto.Deleted;

        db.Companies.Add(CompanyAuthMapper(dto.Company));

        return db;
    }
    public BusinessAuthDto BusinessAuthMapper(BusinessAuth entity)
    {
        var obj = new BusinessAuthDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Deleted = entity.Deleted,
            BusinessProfileId = entity.BusinessProfileId
        };
        return obj;
    }
    public BusinessAuth BusinessAuthMapper(BusinessAuthDto entity)
    {
        var obj = new BusinessAuth()
        {
            Id = entity.Id,
            Name = entity.Name,
            Deleted = entity.Deleted,
            BusinessProfileId = entity.BusinessProfileId
        };
        return obj;
    }
    public List<BusinessAuthDto> BusinessAuthListMake(List<BusinessAuth> list)
    {
        if (list == null) return new();

        var toReturn = new List<BusinessAuthDto>();

        list.ForEach(x =>
        {
            toReturn.Add(BusinessAuthMapper(x));
        });

        return toReturn;
    }
    public List<BusinessAuth> BusinessAuthListMake(List<BusinessAuthDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<BusinessAuth>();

        list.ForEach(x =>
        {
            toReturn.Add(BusinessAuthMapper(x));
        });

        return toReturn;
    }
   
    //  public BusinessProfile BusinessProfileMapperUpdate(BusinessProfile db, BusinessProfileUpdateAddCompanyDto dto)
    // {

    //     if(dto.Company == null) throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);

    //     db.Deleted = dto.Deleted;
    //     db.Companies.Add(CompanyProfileMapper(dto.Company));

    //     return db;
    // }
    public BusinessProfileDto BusinessProfileMapper(BusinessProfile entity)
    {
        var obj = new BusinessProfileDto()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            BusinessAuthId = entity.BusinessAuthId
        };
        return obj;
    }
    public BusinessProfile BusinessProfileMapper(BusinessProfileDto entity)
    {
        var obj = new BusinessProfile()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            BusinessAuthId = entity.BusinessAuthId
        };
        return obj;
    }
    public List<BusinessProfileDto> BusinessProfileListMake(List<BusinessProfile> list)
    {
        if (list == null) return new();

        var toReturn = new List<BusinessProfileDto>();

        list.ForEach(x =>
        {
            toReturn.Add(BusinessProfileMapper(x));
        });

        return toReturn;
    }
    public List<BusinessProfile> BusinessProfileListMake(List<BusinessProfileDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<BusinessProfile>();

        list.ForEach(x =>
        {
            toReturn.Add(BusinessProfileMapper(x));
        });

        return toReturn;
    }

    public UserProfileDto UserProfileMapper(UserProfile entity)
    {
        var obj = new UserProfileDto()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            UserAccountId = entity.UserAccountId
        };
        return obj;
    }
    public UserProfile UserProfileMapper(UserProfileDto entity)
    {
        var obj = new UserProfile()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            UserAccountId = entity.UserAccountId
        };
        return obj;
    }
    public List<UserProfileDto> UserProfileListMake(List<UserProfile> list)
    {
        if (list == null) return new();

        var toReturn = new List<UserProfileDto>();

        list.ForEach(x =>
        {
            toReturn.Add(UserProfileMapper(x));
        });

        return toReturn;
    }
    public List<UserProfile> UserProfileListMake(List<UserProfileDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<UserProfile>();

        list.ForEach(x =>
        {
            toReturn.Add(UserProfileMapper(x));
        });

        return toReturn;
    }


    public CompanyProfileDto CompanyProfileMapper(CompanyProfile entity)
    {
        var obj = new CompanyProfileDto()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            CompanyAuthId = entity.CompanyAuthId
        };
        return obj;
    }
    public CompanyProfile CompanyProfileMapper(CompanyAuth entity)
    {
        var obj = new CompanyProfile()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            CompanyAuthId = entity.CompanyProfileId
        };
        return obj;
    }
    public CompanyProfile CompanyProfileMapper(CompanyProfileDto entity)
    {
        var obj = new CompanyProfile()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            CompanyAuthId = entity.CompanyAuthId,
            Address = AddressMapper(entity.Address ?? new()),
            Contact = ContactMapper(entity.Contact ?? ContactDto.Create(entity.Contact.Id, entity.Contact.Deleted, entity.Contact.Registered, entity.Contact.Email, entity.Contact.Site, entity.Contact.Cel, entity.Contact.Zap, entity.Contact.Landline, [])),
            
        };
        return obj;
    }
    public CompanyAuth CompanyAuthMapper(CompanyAuthDto entity)
    {
        var obj = new CompanyAuth()
        {
            Name = entity.Name,
            Id = entity.Id,
            Deleted = entity.Deleted,
            CompanyProfileId = entity.CompanyProfileId,
            
        };
        return obj;
    }
    public List<CompanyProfileDto> CompanyListMake(List<CompanyProfile> list)
    {
        if (list == null) return new();

        var toReturn = new List<CompanyProfileDto>();

        list.ForEach(x =>
        {
            toReturn.Add(CompanyProfileMapper(x));
        });

        return toReturn;
    }
    public List<CompanyProfile> CompanyListMake(List<CompanyProfileDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<CompanyProfile>();

        list.ForEach(x =>
        {
            toReturn.Add(CompanyProfileMapper(x));
        });

        return toReturn;
    }


    public List<AddressDto> AddressListMake(List<Address> list)
    {
        if (list == null) return new();

        var toReturn = new List<AddressDto>();

        list.ForEach(x =>
        {
            toReturn.Add(AddressMapper(x));
        });

        return toReturn;
    }
    public List<Address> AddressListMake(List<AddressDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<Address>();

        list.ForEach(x =>
        {
            toReturn.Add(AddressMapper(x));
        });


        return toReturn;
    }
    public AddressDto AddressMapper(Address entity)
    {
        if (entity == null) return new();

        var obj = new AddressDto()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            ZipCode = entity.ZipCode,
            Street = entity.Street,
            Number = entity.Number,
            District = entity.District,
            City = entity.City,
            State = entity.State,
            Complement = entity.Complement,

        };

        return obj;
    }
    public Address AddressMapper(AddressDto entity)
    {
        if (entity == null) return new();

        var obj = new Address()
        {
            Id = entity.Id,
            Deleted = entity.Deleted,
            Registered = entity.Registered,
            ZipCode = entity.ZipCode,
            Street = entity.Street,
            Number = entity.Number,
            District = entity.District,
            City = entity.City,
            State = entity.State,
            Complement = entity.Complement,
        };

        return obj;
    }
    public List<ContactDto> ContactListMake(List<Contact> list)
    {
        if (list == null) return new();

        var toReturn = new List<ContactDto>();

        list.ForEach(x =>
        {
            toReturn.Add(ContactMapper(x));
        });

        return toReturn;
    }
    public List<Contact> ContactListMake(List<ContactDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<Contact>();

        list.ForEach(x =>
        {
            toReturn.Add(ContactMapper(x));
        });


        return toReturn;
    }
    public ContactDto ContactMapper(Contact entity)
    {
        if (entity == null) return ContactDto.Create(0,
            DateTime.MinValue,
            DateTime.MinValue,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            new List<SocialNetworkDto>());

        var obj = ContactDto.Create(entity.Id, entity.Deleted, entity.Registered, entity.Email, entity.Site, entity.Cel, entity.Zap, entity.Landline, SocialNetworkListMake(entity.SocialMedias));

        return obj;
    }
    public Contact ContactMapper(ContactDto entity)
    {
        if (entity == null) return new();

        var obj = new Contact()
        {
            Id = entity.Id,
            // CompanyId = entity.CompanyId,
            Deleted = entity.Deleted,
            Registered = entity.Registered,

            Email = entity.Email,
            Site = entity.Site,
            Cel = entity.Cel,
            Zap = entity.Zap,
            Landline = entity.Landline,
            SocialMedias = SocialNetworkListMake(entity.SocialMedias),
        };

        return obj;
    }
    public List<SocialNetworkDto> SocialNetworkListMake(List<SocialNetwork> list)
    {
        if (list == null) return new();

        var toReturn = new List<SocialNetworkDto>();

        list.ForEach(x =>
        {
            toReturn.Add(SocialNetworkMapper(x));
        });

        return toReturn;
    }
    public List<SocialNetwork> SocialNetworkListMake(List<SocialNetworkDto> list)
    {
        if (list == null) return new();

        var toReturn = new List<SocialNetwork>();

        list.ForEach(x =>
        {
            toReturn.Add(SocialNetworkMapper(x));
        });


        return toReturn;
    }
    public SocialNetworkDto? SocialNetworkMapper(SocialNetwork entity)
    {
        if (entity == null) return null;

        var obj = new SocialNetworkDto()
        {
            Id = entity.Id,
            // CompanyId = entity.CompanyId,
            Deleted = entity.Deleted,
            Registered = entity.Registered,

            Name = entity.Name,
            Url = entity.Url,
            ContactId = entity.ContactId,

        };

        return obj;
    }
    public SocialNetwork SocialNetworkMapper(SocialNetworkDto entity)
    {
        if (entity == null) return new();

        var obj = new SocialNetwork()
        {
            Id = entity.Id,
            // CompanyId = entity.CompanyId,
            Deleted = entity.Deleted,
            Registered = entity.Registered,

            Name = entity.Name,
            Url = entity.Url,
            ContactId = entity.ContactId,

        };

        return obj;
    }

}