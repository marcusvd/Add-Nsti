using Application.Services.Operations.Companies.Dtos;
using Domain.Entities.System.BusinessesCompanies;

using Domain.Entities.Shared;
using Application.Services.Operations.Profiles.Dtos;
using Domain.Entities.System.Profiles;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;


namespace Application.Services.Shared.Dtos.Mappers;

public interface ICommonObjectMapper
{
    BusinessAuthDto BusinessAuthMapper(BusinessAuth entity);
    BusinessAuth BusinessAuthMapper(BusinessAuthDto entity);
    BusinessAuth BusinessAuthMapperUpdate(BusinessAuth db, BusinessAuthUpdateAddCompanyDto dto);
    List<BusinessAuthDto> BusinessAuthListMake(List<BusinessAuth> list);
    List<BusinessAuth> BusinessAuthListMake(List<BusinessAuthDto> list);


    UserProfile UserProfileMapper(UserProfileDto entity);
    UserProfileDto UserProfileMapper(UserProfile entity);
    List<UserProfileDto> UserProfileListMake(List<UserProfile> list);
    List<UserProfile> UserProfileListMake(List<UserProfileDto> list);

    BusinessProfile BusinessProfileMapper(BusinessProfileDto entity);
    BusinessProfileDto BusinessProfileMapper(BusinessProfile entity);
    List<BusinessProfileDto> BusinessProfileListMake(List<BusinessProfile> list);
    List<BusinessProfile> BusinessProfileListMake(List<BusinessProfileDto> list);

    CompanyProfileDto CompanyProfileMapper(CompanyProfile entity);
    CompanyProfile CompanyProfileMapper(CompanyProfileDto entity);
    CompanyProfile CompanyProfileMapper(CompanyAuth entity);
    CompanyAuth CompanyAuthMapper(CompanyAuthDto entity);
    List<CompanyProfileDto> CompanyListMake(List<CompanyProfile> list);
    List<CompanyProfile> CompanyListMake(List<CompanyProfileDto> list);

    List<AddressDto> AddressListMake(List<Address> list);
    List<Address> AddressListMake(List<AddressDto> list);
    AddressDto AddressMapper(Address entity);
    Address AddressMapper(AddressDto entity);

    List<ContactDto> ContactListMake(List<Contact> list);
    List<Contact> ContactListMake(List<ContactDto> list);
    ContactDto ContactMapper(Contact entity);
    Contact ContactMapper(ContactDto entity);

    List<SocialNetworkDto> SocialNetworkListMake(List<SocialNetwork> list);
    List<SocialNetwork> SocialNetworkListMake(List<SocialNetworkDto> list);
    SocialNetworkDto? SocialNetworkMapper(SocialNetwork entity);
    SocialNetwork? SocialNetworkMapper(SocialNetworkDto entity);

}