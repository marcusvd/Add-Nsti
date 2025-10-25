
// 
// using Domain.Entities.Shared;
// using Domain.Entities.Authentication;
// 
// using Application.Shared.Dtos;
// using Application.CompaniesServices.Dtos;

// namespace Application.Shared.Mappers.BaseMappers;

// public class CompanyProfileEntityMapper : BaseMapper<CompanyProfile, CompanyProfileDto>
// {
//     private readonly IMapper<Address, AddressDto> _addressMapper;
//     private readonly IMapper<Contact, ContactDto> _contactMapper;
//     public CompanyProfileEntityMapper(
//             IMapper<Address, AddressDto> addressMapper,
//             IMapper<Contact, ContactDto> contactMapper
//     )
//     {
//         _addressMapper = addressMapper;
//         _contactMapper = contactMapper;
//     }

//     public override CompanyProfileDto Map(CompanyProfile source)
//     {
//         if (source == null) return new CompanyProfileDto() { BusinessProfileId = 0,  CNPJ = "invalid" };

//         var destination = base.Map(source);

//         destination.Address = _addressMapper.Map(source.Address = AddressMapper.Incomplete().ToEntity());
//         destination.Contact = _contactMapper.Map(source.Contact = new Contact());
//         destination.CNPJ = source.CNPJ;
//         return destination;
//     }

// }

// public class CompanyProfileDtoMapper : BaseMapper<CompanyProfileDto, CompanyProfile>
// {
//     private readonly IMapper<AddressDto, Address> _addressDtoMapper;
//     private readonly IMapper<ContactDto, Contact> _contactDtoMapper;

//     public CompanyProfileDtoMapper(
//             IMapper<AddressDto, Address> addressDtoMapper,
//             IMapper<ContactDto, Contact> contactDtoMapper
//     )
//     {
//         _addressDtoMapper = addressDtoMapper;
//         _contactDtoMapper = contactDtoMapper;
//     }

//     public override CompanyProfile Map(CompanyProfileDto source)
//     {
//         if (source == null) return new CompanyProfile() {  CNPJ = "invalid" };

//         var destination = base.Map(source);

//         destination.Address = _addressDtoMapper.Map(source.Address ?? AddressMapper.Incomplete());
//         destination.Contact = _contactDtoMapper.Map(source.Contact ?? new ContactDto());
//         destination.CNPJ = source.CNPJ;
//         return destination;
//     }

// }

// public class CompanyAuthEntityMapper : BaseMapper<CompanyAuth, CompanyAuthDto>
// {
//     private readonly IMapper<CompanyUserAccount, CompanyUserAccountDto> _companyUserAccountEntityMapper;
//     public CompanyAuthEntityMapper(IMapper<CompanyUserAccount, CompanyUserAccountDto> companyUserAccountEntityMapper)
//     {
//         _companyUserAccountEntityMapper = companyUserAccountEntityMapper;
//     }

//     public override CompanyAuthDto Map(CompanyAuth source)
//     {
//         if (source == null) return new CompanyAuthDto() { Name = "invalid", TradeName = "invalid", CNPJ = "invalid" };

//         var destination = base.Map(source);

//         destination.CompanyUserAccounts = _companyUserAccountEntityMapper.Map(source.CompanyUserAccounts).ToList();


//         return destination;
//     }

// }
// public class CompanyAuthDtoMapper : BaseMapper<CompanyAuthDto, CompanyAuth>
// {
//     private readonly IMapper<CompanyUserAccountDto, CompanyUserAccount> _companyUserAccountDtoMapper;

//     public CompanyAuthDtoMapper(IMapper<CompanyUserAccountDto, CompanyUserAccount> companyUserAccountDtoMapper)
//     {
//         _companyUserAccountDtoMapper = companyUserAccountDtoMapper;
//     }
//     public override CompanyAuth Map(CompanyAuthDto source)
//     {
//         if (source == null) return new CompanyAuth() { Name = "invalid", TradeName = "invalid", CNPJ = "invalid" };

//         var destination = base.Map(source);

//         destination.CompanyUserAccounts = _companyUserAccountDtoMapper.Map(source.CompanyUserAccounts).ToList();

//         return destination;
//     }

// }