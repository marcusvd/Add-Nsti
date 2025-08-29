using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.Shared;
using Domain.Entities.System.Profiles;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Application.Services.Operations.Companies.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Shared.Mappers.BaseMappers;

public class CommonObjectMapper : ICommonObjectMapper
{
    private readonly Dictionary<Type, object> _mappers = new Dictionary<Type, object>();
    private readonly IMapper<Address, AddressDto> _addressEntityMapper;
    private readonly IMapper<AddressDto, Address> _addressDtoMapper;

    private readonly IMapper<Contact, ContactDto> _contactEntityMapper;
    private readonly IMapper<ContactDto, Contact> _contactDtoMapper;

    private readonly IMapper<UserAccountDto, UserAccount> _userAccountDtoMapper;
    private readonly IMapper<UserAccount, UserAccountDto> _userAccountEntityMapper;

    // private readonly IMapper<CompanyAuthDto, CompanyAuth> _companyAuthDtoMapper;
    // private readonly IMapper<CompanyAuth, CompanyAuthDto> _companyAuthEntityMapper;

    // private readonly IMapper<BusinessAuthDto, BusinessAuth> _businessAuthDtoMapper;
    // private readonly IMapper<BusinessAuth, BusinessAuthDto> _businessAuthEntityMapper;

    // private readonly IMapper<CompanyUserAccountDto, CompanyUserAccount> _companyUserAccountDtoMapper;
    // private readonly IMapper<CompanyUserAccount, CompanyUserAccountDto> _companyUserAccountEntityMapper;



    public CommonObjectMapper(

            IMapper<Address, AddressDto> addressEntityMapper,
            IMapper<AddressDto, Address> addressDtoMapper,

            IMapper<Contact, ContactDto> contactEntityMapper,
            IMapper<ContactDto, Contact> contactDtoMapper,

            IMapper<UserAccount, UserAccountDto> userAccountEntityMapper,
            IMapper<UserAccountDto, UserAccount> userAccountDtoMapper

            // IMapper<CompanyAuth, CompanyAuthDto> companyAuthEntityMapper,
            // IMapper<CompanyAuthDto, CompanyAuth> companyAuthDtoMapper

            // IMapper<BusinessAuthDto, BusinessAuth> businessAuthDtoMapper,
            // IMapper<BusinessAuth, BusinessAuthDto> businessAuthEntityMapper

            // IMapper<CompanyUserAccountDto, CompanyUserAccount> companyUserAccountDtoMapper,
            // IMapper<CompanyUserAccount, CompanyUserAccountDto> companyUserAccountEntityMapper

            )
    {
        _addressEntityMapper = addressEntityMapper;
        _addressDtoMapper = addressDtoMapper;

        _contactEntityMapper = contactEntityMapper;
        _contactDtoMapper = contactDtoMapper;

        _userAccountEntityMapper = userAccountEntityMapper;
        _userAccountDtoMapper = userAccountDtoMapper;

        // _companyAuthEntityMapper = companyAuthEntityMapper;
        // _companyAuthDtoMapper = companyAuthDtoMapper;

        // _businessAuthDtoMapper = businessAuthDtoMapper;
        // _businessAuthEntityMapper = businessAuthEntityMapper;

        // _companyUserAccountDtoMapper = companyUserAccountDtoMapper;
        // _companyUserAccountEntityMapper = companyUserAccountEntityMapper;

        RegisterMappers();
    }

    private void RegisterMappers()
    {
        _mappers[typeof(Address)] = new AddressEntityMapper();
        _mappers[typeof(AddressDto)] = new AddressDtoMapper();

        _mappers[typeof(Contact)] = new ContactEntityMapper();
        _mappers[typeof(ContactDto)] = new ContactDtoMapper();

        _mappers[typeof(UserAccount)] = new UserAccountEntityMapper();
        _mappers[typeof(UserAccountDto)] = new UserAccountDtoMapper();

        _mappers[typeof(UserProfile)] = new UserProfileEntityMapper(_addressEntityMapper, _contactEntityMapper);
        _mappers[typeof(UserProfileDto)] = new UserProfileDtoMapper(_addressDtoMapper, _contactDtoMapper);

        // _mappers[typeof(CompanyAuth)] = new CompanyAuthEntityMapper(_companyUserAccountEntityMapper);
        // _mappers[typeof(CompanyAuthDto)] = new CompanyAuthDtoMapper(_companyUserAccountDtoMapper);

        _mappers[typeof(CompanyProfile)] = new CompanyProfileEntityMapper(_addressEntityMapper, _contactEntityMapper);
        _mappers[typeof(CompanyProfileDto)] = new CompanyProfileDtoMapper(_addressDtoMapper, _contactDtoMapper);

        // _mappers[typeof(BusinessAuth)] = new BusinessAuthEntityMapper(_userAccountEntityMapper, _companyAuthEntityMapper);
        // _mappers[typeof(BusinessAuthDto)] = new BusinessAuthDtoMapper(_userAccountDtoMapper, _companyAuthDtoMapper);

        // _mappers[typeof(CompanyUserAccount)] = new CompanyUserAccountEntityMapper(_userAccountEntityMapper, _companyAuthEntityMapper);
        // _mappers[typeof(CompanyUserAccountDto)] = new CompanyUserAccountDtoMapper(_userAccountDtoMapper, _companyAuthDtoMapper);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null) return default;

        if (_mappers.TryGetValue(typeof(TSource), out var mapperObj) &&
            mapperObj is IMapper<TSource, TDestination> mapper)
        {
            return mapper.Map(source);
        }

        return new BaseMapper<TSource, TDestination>().Map(source);
    }
    public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> sources)
    {
        return sources?.Select(Map<TSource, TDestination>) ?? Enumerable.Empty<TDestination>();
    }
    public TSource Map<TSource, TDestination>(TDestination source)
    {
        if (source == null) return default;

        if (_mappers.TryGetValue(typeof(TSource), out var mapperObj) &&
            mapperObj is IMapper<TSource, TDestination> mapper)
        {
            return mapper.Map(source);
        }

        return new BaseMapper<TSource, TDestination>().Map(source);
    }
    public IEnumerable<TSource> Map<TSource, TDestination>(IEnumerable<TDestination> destinations)
    {
        return destinations?.Select(Map<TDestination, TSource>) ?? Enumerable.Empty<TSource>();
    }
}

public static class DiObjsMappers
{
    public static void DiMappers(this IServiceCollection services)
    {
        services.AddScoped<IMapper<Address, AddressDto>, AddressEntityMapper>();
        services.AddScoped<IMapper<AddressDto, Address>, AddressDtoMapper>();

        services.AddScoped<IMapper<Contact, ContactDto>, ContactEntityMapper>();
        services.AddScoped<IMapper<ContactDto, Contact>, ContactDtoMapper>();

        // services.AddScoped<IMapper<CompanyUserAccount, CompanyUserAccountDto>, CompanyUserAccountEntityMapper>();
        // services.AddScoped<IMapper<CompanyUserAccountDto, CompanyUserAccount>, CompanyUserAccountDtoMapper>();

        // services.AddScoped<IMapper<CompanyAuth, CompanyAuthDto>, CompanyAuthEntityMapper>();
        // services.AddScoped<IMapper<CompanyAuthDto, CompanyAuth>, CompanyAuthDtoMapper>();

        services.AddScoped<IMapper<CompanyProfile, CompanyProfileDto>, CompanyProfileEntityMapper>();
        services.AddScoped<IMapper<CompanyProfileDto, CompanyProfile>, CompanyProfileDtoMapper>();

        services.AddScoped<IMapper<UserAccount, UserAccountDto>, UserAccountEntityMapper>();
        services.AddScoped<IMapper<UserAccountDto, UserAccount>, UserAccountDtoMapper>();

        services.AddScoped<IMapper<UserProfile, UserProfileDto>, UserProfileEntityMapper>();
        services.AddScoped<IMapper<UserProfileDto, UserProfile>, UserProfileDtoMapper>();

        // services.AddScoped<IMapper<BusinessAuth, BusinessAuthDto>, BusinessAuthEntityMapper>();
        // services.AddScoped<IMapper<BusinessAuthDto, BusinessAuth>, BusinessAuthDtoMapper>();

    }
}