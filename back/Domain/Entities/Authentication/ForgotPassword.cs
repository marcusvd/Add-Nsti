
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Authentication;

public class ForgotPassword
{
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}