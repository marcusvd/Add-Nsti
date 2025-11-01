using System.ComponentModel.DataAnnotations;

namespace Application.Auth.Register.Dtos.FirstRegister;

public class FirstConfirmEmailRegisterDto
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}


