using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;


namespace Authentication.Operations.AuthAdm;

public interface IAuthAdmServices
{
        Task<BusinessAuthDto> GetBusinessFullAsync(int id);
        Task<BusinessAuth> GetBusinessAsync(int id);
        Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id);
}