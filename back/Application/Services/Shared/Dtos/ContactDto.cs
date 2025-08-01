using Domain.Entities.Companies;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Dtos
{
    public class ContactDto : RootBase
    {
        public ContactDto(
            int id,
            Company? company,
            int companyId,
            DateTime deleted,
            DateTime registered,
            string email,
            string site,
            string cel,
            string zap,
            string landline,
            List<SocialNetworkDto> socialMedias
        )
        {
            Id = id;
            Company = company;
            CompanyId = companyId;
            Deleted = deleted;
            Registered = registered;
            Email = email;
            Site = site;
            Cel = cel;
            Zap = zap;
            Landline = landline;
            SocialMedias = socialMedias;
        }

        public string Email { get; set; }
        public string Site { get; set; }
        public string Cel { get; set; }
        public string Zap { get; set; }
        public string Landline { get; set; }
        public List<SocialNetworkDto> SocialMedias { get; set; }
        public static ContactDto Create(
            int id,
            Company? company,
            int companyId,
            DateTime deleted,
            DateTime registered,
            string email,
            string site,
            string cel,
            string zap,
            string landline,
            List<SocialNetworkDto> socialMedias)
        {
            // Aqui você pode adicionar validações, normalizações, etc.
            return new ContactDto(id, company, companyId, deleted, registered, email, site, cel, zap, landline, socialMedias);
        }



    }
}