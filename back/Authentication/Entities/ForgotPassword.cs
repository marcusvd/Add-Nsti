
using System.ComponentModel.DataAnnotations;

namespace Authentication.Entities;

public class ForgotPassword
{
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
}