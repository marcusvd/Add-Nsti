using Application.Auth.Roles.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Application.Auth.Roles.Services;

public interface IRolesServices
{
    Task<string[]> UpdateUserRoles(UpdateUserRoleDto[] roles);
    Task<IList<string>> GetRolesAsync(UserAccount userAccount);
    Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
    Task<UpdateUserRoleDto[]> AddSubscriberRules(string email);

}