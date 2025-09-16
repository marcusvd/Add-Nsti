namespace Application.Services.Operations.Auth.Dtos;
public class ToggleTwoFactorDto
{
  public int UserId { get; set; } = 0;
  public bool Enable { get; set; } = false;
}
