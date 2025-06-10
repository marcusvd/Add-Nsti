namespace Application.Services.Shared.Dtos
{
    public class SocialNetworkDto : RootBaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int ContactId { get; set; }
        public ContactDto Contact { get; set; }
    }
}