using Domain.Entities.Authentication;


namespace Application.UsersAccountsServices.Dtos.Roles;

public class UserRoleDto
{
    public UserAccount UserAccount { get; set; }
    public Role Role { get; set; }

    public static implicit operator UserRole(UserRoleDto dto)
    {
        return new()
        {
            UserAccount = dto.UserAccount,
            Role = dto.Role
        };
    }
    
    public static implicit operator UserRoleDto(UserRole dto)
    {
        return new()
        {
            UserAccount = dto.UserAccount,
            Role = dto.Role
        };
    }

}