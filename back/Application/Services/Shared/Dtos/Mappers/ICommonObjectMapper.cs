using System.Collections.Generic;


using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Operations.Main.Companies.Dtos;
using Domain.Entities.Companies;
using Domain.Entities.Authentication;
using Domain.Entities.Shared;


namespace Application.Services.Shared.Dtos.Mappers
{
    public interface ICommonObjectMapper
    {
        CompanyDto CompanyMapper(Company entity);
        Company CompanyMapper(CompanyDto entity);
        List<CompanyDto> CompanyListMake(List<Company> list);
        List<Company> CompanyListMake(List<CompanyDto> list);

        MyUserDto MyUserMapper(MyUser entity);
        MyUser MyUserMapper(MyUserDto entity);
        List<MyUserDto> MyUserListMake(List<MyUser> list);
        List<MyUser> MyUserListMake(List<MyUserDto> list);

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
        SocialNetworkDto SocialNetworkMapper(SocialNetwork entity);
        SocialNetwork SocialNetworkMapper(SocialNetworkDto entity);
     
    }
}