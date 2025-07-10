using System;

namespace Domain.Entities.Authentication
{
    public class UserToken
    {
        public bool Authenticated {get; set;}
        public DateTime Expiration {get; set;}
        public string Token {get; set;}
        public int Id {get; set;}
        public string UserName {get; set;}
        public int CompanyId {get; set;}
        public string Action {get; set;}
    }
}