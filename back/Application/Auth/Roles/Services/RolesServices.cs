
using Microsoft.AspNetCore.Identity;

using Application.Auth.Extends;
using Application.Auth.Roles.Dtos;
using Application.Auth.UsersAccountsServices.Auth;
using Domain.Entities.Authentication;

namespace Application.Auth.Roles.Services;

public class RolesServices : AuthenticationBase, IRolesServices
{
    private readonly UserAccountAuthServices _userAccountAuthServices;
    private UserManager<UserAccount> _usersManager { get; }
    private RoleManager<Role> _rolesManager { get; }

    public RolesServices(
     UserManager<UserAccount> usersManager,
     RoleManager<Role> rolesManager,
     UserAccountAuthServices userAccountAuthServices
    )
    {
        _usersManager = usersManager;
        _rolesManager = rolesManager;
        _userAccountAuthServices = userAccountAuthServices;
    }

    public async Task<string[]> UpdateUserRoles(UpdateUserRoleDto[] roles)
    {
        var results = new List<string>();

        foreach (var role in roles)
        {
            string email = IsValidEmail(role.UserName);

            var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

            if (role.Delete)
            {
                await _usersManager.RemoveFromRoleAsync(userAccount, role.Role);
                results.Add("Role removed");
            }
            else
            {
                if (!await _usersManager.IsInRoleAsync(userAccount, role.Role))
                    await _usersManager.AddToRoleAsync(userAccount, role.Role);

                results.Add("Role Added");
            }
        }

        return results.ToArray();
    }

    public async Task<IList<string>> GetRolesAsync(UserAccount userAccount) => await _usersManager.GetRolesAsync(userAccount);
    public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto) => await _rolesManager.CreateAsync(new Role { Name = roleDto.Name, DisplayRole = roleDto.DisplayRole });
    public async Task<UpdateUserRoleDto[]> AddSubscriberRules(string email)
    {

        UpdateUserRoleDto[] rolesSubscriber =
        [new UpdateUserRoleDto { UserName = email, Role = "HOLDER", DisplayRole = "Acesso Total", Delete = false }, new UpdateUserRoleDto { UserName = email, Role = "SYSADMIN", DisplayRole = "Administrador", Delete = false }];

        return await Task.FromResult(rolesSubscriber);
    }

}