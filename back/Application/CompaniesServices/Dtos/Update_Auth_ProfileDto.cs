

using Application.CompaniesServices.Dtos.Auth;
using Application.CompaniesServices.Dtos.Profile;

namespace Application.CompaniesServices.Dtos;

public class Update_Auth_ProfileDto
{
    public required int CompanyAuthId { get; set; }
    public required int CompanyProfileId { get; set; }
    public required CompanyAuthDto CompanyAuth { get; set; }
    public required CompanyProfileDto CompanyProfile { get; set; }

}