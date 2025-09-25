using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Operations.Auth.Account.dtos;

public class TwoFactorCheckViewModel
{
    public required string Token { get; set; } = string.Empty;

    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required string SelectedProvider { get; set; }
    public IList<string>? Providers { get; set; }
    public bool RememberMe { get; set; } = true;


}