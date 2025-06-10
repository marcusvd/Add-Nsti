namespace Application.Services.Operations.Authentication.Dtos
{
     public class UpdateUserRoleDto
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public bool Delete { get; set; }
    }
}