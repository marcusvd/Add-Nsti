using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Operations.Account;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Application.Services.Shared.Operations;
using Authentication.Operations.AuthAdm;

namespace Application.Services.Helpers.ServicesLauncher
{
    public interface IServiceLaucherService
    {
        IAccountManagerServices AccountManagerServices { get; }
        IAuthAdmServices AuthAdmServices { get; }
        ICompanyAuthServices CompanyAuthServices { get; }
        IRegisterUserAccountServices RegisterUserAccountServices { get; }
        ILoginServices LoginServices { get; }
        IFirstRegisterBusinessServices RegisterServices { get; }
        IAddressServices AddressServices { get; }
        IContactServices ContactServices { get; }
    }
}