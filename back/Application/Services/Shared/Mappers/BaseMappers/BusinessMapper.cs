using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Operations.Profiles.Dtos;

namespace Application.Services.Shared.Mappers.BaseMappers;

public class BusinessAuthEntityMapper : BaseMapper<BusinessAuth, BusinessAuthDto>
{
    private readonly IMapper<UserAccount, UserAccountDto> _userAccountEntityMapper;
    private readonly IMapper<CompanyAuth, CompanyAuthDto> _companyAuthEntityMapper;


    public BusinessAuthEntityMapper(
         IMapper<UserAccount, UserAccountDto> userAccountEntityMapper,
         IMapper<CompanyAuth, CompanyAuthDto> companyAuthEntityMapper
     )
    {
        _userAccountEntityMapper = userAccountEntityMapper;
        _companyAuthEntityMapper = companyAuthEntityMapper;
    }

    public override BusinessAuthDto Map(BusinessAuth source)
    {
        if (source == null) return new BusinessAuthDto() { Name = "invalid", BusinessProfileId = "invalid" };

        var destination = base.Map(source);

        destination.UsersAccounts = _userAccountEntityMapper.Map(source.UsersAccounts).ToList();
        destination.Companies = _companyAuthEntityMapper.Map(source.Companies).ToList();

        return destination;
    }
}
public class BusinessAuthDtoMapper : BaseMapper<BusinessAuthDto, BusinessAuth>
{
    private readonly IMapper<UserAccountDto, UserAccount> _userAccountDtoMapper;
    private readonly IMapper<CompanyAuthDto, CompanyAuth> _companyAuthDtoMapper;


    public BusinessAuthDtoMapper(
         IMapper<UserAccountDto, UserAccount> userAccountDtoMapper,
         IMapper<CompanyAuthDto, CompanyAuth> companyAuthDtoMapper
     )
    {
        _userAccountDtoMapper = userAccountDtoMapper;
        _companyAuthDtoMapper = companyAuthDtoMapper;
    }

    public override BusinessAuth Map(BusinessAuthDto source)
    {
        if (source == null) return new BusinessAuth() {Id = -0, Name = "invalid", BusinessProfileId = "invalid" };

        var destination = base.Map(source);

        destination.UsersAccounts = _userAccountDtoMapper.Map(source.UsersAccounts).ToList();
        destination.Companies = _companyAuthDtoMapper.Map(source.Companies).ToList();

        return destination;
    }
}


public class BusinessProfileEntityMapper : BaseMapper<BusinessProfile, BusinessProfileDto>
{
    public override BusinessProfileDto Map(BusinessProfile source)
    {
        if (source == null) return new BusinessProfileDto() { BusinessAuthId = "invalid" };

        var destination = base.Map(source);

        return destination;
    }
}
public class BusinessProfileDtoMapper : BaseMapper<BusinessProfileDto, BusinessProfile>
{
    public override BusinessProfile Map(BusinessProfileDto source)
    {
        if (source == null) return new BusinessProfile() {Id = -1, BusinessAuthId = "invalid" };

        var destination = base.Map(source);

        return destination;
    }
}