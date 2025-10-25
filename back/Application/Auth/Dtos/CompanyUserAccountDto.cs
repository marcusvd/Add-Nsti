
using Application.CompaniesServices.Dtos.Auth;
using Application.Shared.Dtos;
using Application.Shared.Validators;
using Application.UsersAccountsServices.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.Dtos;


public class CompanyUserAccountDto : RootBaseDto
{
    public int CompanyAuthId { get; set; }
    public CompanyAuthDto CompanyAuth { get; set; } = (CompanyAuthDto)GenericValidators.ReplaceNullObj<CompanyAuthDto>();

    public int UserAccountId { get; set; }
    public UserAccountDto UserAccount { get; set; } = (UserAccountDto)GenericValidators.ReplaceNullObj<UserAccountDto>();

    public DateTime LinkedOn { get; set; } = DateTime.UtcNow;

    public static implicit operator CompanyUserAccount(CompanyUserAccountDto dto)
    {
        return new()
        {
            Deleted = dto.Deleted,
            Registered = dto.Registered,
            CompanyAuthId = dto.CompanyAuthId,
            UserAccountId = dto.UserAccountId,
            LinkedOn = dto.LinkedOn,
        };
    }
    public static implicit operator CompanyUserAccountDto(CompanyUserAccount db)
    {
        return new()
        {
            Deleted = db.Deleted,
            Registered = db.Registered,
            CompanyAuthId = db.CompanyAuthId,
            UserAccountId = db.UserAccountId,
            LinkedOn = db.LinkedOn,
        };
    }
}

// public static class CompanyUserAccountMapper
// {
//     public static CompanyUserAccount ToEntity(this CompanyUserAccountDto dto)
//     {
//         CompanyUserAccount address = new()
//         {
//             Deleted = dto.Deleted,
//             Registered = dto.Registered,
//             CompanyAuthId = dto.CompanyAuthId,
//             // CompanyAuth = dto.CompanyAuth.ToEntity(),
//             UserAccountId = dto.UserAccountId,
//             // UserAccount = dto.UserAccount.ToEntity(),
//             LinkedOn = dto.LinkedOn,
//         };

//         return address;
//     }

//     public static CompanyUserAccountDto ToDto(this CompanyUserAccount entity)
//     {
//         CompanyUserAccountDto address = new()
//         {
//             Deleted = entity.Deleted,
//             Registered = entity.Registered,
//             CompanyAuthId = entity.CompanyAuthId,
//             // CompanyAuth = entity.CompanyAuth.ToDto() ?? CompanyAuthMapper.Incomplete(),
//             UserAccountId = entity.UserAccountId,
//             // UserAccount = entity.UserAccount.ToDto() ?? UserAccountMapper.Incomplete(),
//             LinkedOn = entity.LinkedOn,

//         };
//         return address;
//     }

    // public static CompanyUserAccountDto Incomplete()
    // {
    //     CompanyUserAccountDto incomplete = new()
    //     {
    //         CompanyAuthId = 0,
    //         // CompanyAuth = CompanyAuthMapper.Incomplete(),
    //         UserAccountId = 0,
    //         // UserAccount = UserAccountMapper.Incomplete(),
    //         LinkedOn = DateTime.Now
    //     };

    //     return incomplete;
    // }


// }