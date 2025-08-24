using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;


namespace Authentication.Operations.AuthAdm;

public interface IAuthAdmServices
{
        Task<BusinessAuth> BusinessAsync(int id);
        Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id);
}