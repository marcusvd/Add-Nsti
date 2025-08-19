using Authentication.Entities;


namespace Authentication.Operations.AuthAdm;

public interface IAuthAdmServices
{
        Task<Business> BusinessAsync(int id);
}