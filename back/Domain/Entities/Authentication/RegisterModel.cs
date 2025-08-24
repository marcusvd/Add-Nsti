namespace Domain.Entities.Authentication;

public class RegisterModel
{
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string CompanyName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    
}