using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;


namespace Application.BusinessesServices.Services.Gateway;

public interface IBusinessServices
{
        Task<bool> UpdateBusinessAuthAndProfileAsync(BusinessAuthUpdateAddCompanyDto businessAuthUpdateDto, int id);
}