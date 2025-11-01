using Application.Auth.Register.Dtos.FirstRegister;
using Application.Helpers.Tools.Cnpj;
using Application.Helpers.Tools.ZipCode;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.Register.Services;

public interface IFirstRegisterBusinessServices
{
        Task<UserToken> RegisterAsync(RegisterModelDto request, ICpfCnpjGetDataServices cpfCnpjGetDataServices, IZipCodeGetDataServices zipCodeGetDataServices, IPhoneNumberValidateServices phoneNumberValidateServices);
        Task<ApiResponse<UserToken>> FirstEmailConfirmation(string email);
}