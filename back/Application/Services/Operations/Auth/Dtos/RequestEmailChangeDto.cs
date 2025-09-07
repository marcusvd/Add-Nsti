namespace Application.Services.Operations.Auth.Account.dtos;

public class RequestEmailChangeDto
{
    public required string OldEmail { get; set; }
    public required string NewEmail { get; set; }
}