using Application.Auth.Register.Dtos.Extends;

namespace Application.Auth.Register.Dtos;

public class AddUserExistingCompanyDto : RegisterBaseDto
{
    public int companyAuthId { get; set; }

}