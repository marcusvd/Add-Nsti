using System.Collections.Generic;


namespace Domain.Entities.Shared
{
    public class Contact: RootBaseDb
    {
        public string? Email { get; set; } = string.Empty;
        public string? Site { get; set; } = string.Empty;
        public string? Cel { get; set; } = string.Empty;
        public string? Zap { get; set; } = string.Empty;
        public string? Landline { get; set; } = string.Empty;
        public List<SocialNetwork>? SocialMedias { get; set; } = new List<SocialNetwork>();

    }
}