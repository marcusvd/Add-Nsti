
// 
// using Domain.Entities.Shared;
// using Domain.Entities.Authentication;
// 
// using Application.Shared.Dtos;

// namespace Application.Shared.Mappers.BaseMappers;

// public class CompanyUserAccountEntityMapper : BaseMapper<CompanyUserAccount, CompanyUserAccountDto>
// {
//     // private readonly IMapper<UserAccount, UserAccountDto> _userAccountMapper;
//     // private readonly IMapper<CompanyAuth, CompanyAuthDto> _CompanyAuthMapper;
//     public CompanyUserAccountEntityMapper(
//             // IMapper<UserAccount, UserAccountDto> UserAccountMapper
//             // IMapper<CompanyAuth, CompanyAuthDto> CompanyAuthMapper
//     )
//     {
//         // _userAccountMapper = UserAccountMapper;
//         // _CompanyAuthMapper = CompanyAuthMapper;
//     }

//     public override CompanyUserAccountDto Map(CompanyUserAccount source)
//     {
//         if (source == null) return new CompanyUserAccountDto() { CompanyAuthId = -1 };

//         var destination = base.Map(source);

//         // destination.UserAccount = _userAccountMapper.Map(source.UserAccount = new UserAccount(){DisplayUserName = "invalid",UserProfileId = "invalid", Email = "invalid"});
//         // destination.CompanyAuth = _CompanyAuthMapper.Map(source.CompanyAuth = new CompanyAuth(){CompanyProfileId = "Invalid" ,Name = "invalid", TradeName = "invalid"});

//         return destination;
//     }

// }

// public class CompanyUserAccountDtoMapper : BaseMapper<CompanyUserAccountDto, CompanyUserAccount>
// {
//     // private readonly IMapper<UserAccountDto, UserAccount> _userAccountDtoMapper;
//     // private readonly IMapper<CompanyAuthDto, CompanyAuth> _companyAuthDtoMapper;

//     public CompanyUserAccountDtoMapper(

//             // IMapper<UserAccountDto, UserAccount> UserAccountDtoMapper
//             // IMapper<CompanyAuthDto, CompanyAuth> companyAuthDtoMapper
//     )
//     {
//         // _userAccountDtoMapper = UserAccountDtoMapper;
//         // _companyAuthDtoMapper = companyAuthDtoMapper;
//     }

//     public override CompanyUserAccount Map(CompanyUserAccountDto source)
//     {
//         if (source == null) return new CompanyUserAccount() { CompanyAuthId = -1 };

//         var destination = base.Map(source);

//         // destination.UserAccount = _userAccountDtoMapper.Map(source.UserAccount ?? new UserAccountDto(){DisplayUserName = "invalid",UserProfileId = "invalid", Email = "invalid"});

//         // destination.CompanyAuth = _companyAuthDtoMapper.Map(source.CompanyAuth ?? new CompanyAuthDto(){CompanyProfileId = "Invalid" ,Name = "invalid", TradeName = "invalid"});
//         return destination;
//     }

// }

