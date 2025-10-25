namespace Domain.Entities.Shared
{
    public class SocialNetwork: RootBaseDb
    {
        public SocialNetwork()
        {

        }
        public SocialNetwork(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

    }
}