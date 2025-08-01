namespace Application.Services.Shared.Dtos
{
    public class SocialNetworkDto : RootBaseDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public int ContactId { get; set; }
        public ContactDto? Contact { get; set; }
    }
}