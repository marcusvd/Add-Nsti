using Authentication;


namespace Application.Services.Operations.Authentication.Dtos.Mappers;

public interface IAuthenticationObjectMapperServices
{
    UserAccount RegisterMapper(RegisterDto entity);
    UserAccountDto UserAccountMapper(UserAccount entity);
}