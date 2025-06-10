using System.Collections.Generic;


namespace Domain.Entities.Shared
{
    public class Contact: RootBase
    {
        public string Email { get; set; }
        public string Site { get; set; }
        public string Cel { get; set; }
        public string Zap { get; set; }
        public string Landline { get; set; }
        public List<SocialNetwork> SocialMedias { get; set; } = new List<SocialNetwork>();

    }
}