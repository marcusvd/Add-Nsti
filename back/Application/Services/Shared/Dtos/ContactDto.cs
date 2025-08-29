using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Dtos
{
    public class ContactDto : RootBase
    {
        public ContactDto(){}

        public string Email { get; set; }
        public string Site { get; set; }
        public string Cel { get; set; }
        public string Zap { get; set; }
        public string Landline { get; set; }
        public List<SocialNetworkDto> SocialMedias { get; set; }

    }
}