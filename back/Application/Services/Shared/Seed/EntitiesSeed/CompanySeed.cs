

using System.Linq;
using Domain.Entities.System.BusinessesCompanies;
using Domain.Entities.Shared;

namespace Application.Services.Shared.Seed.EntitiesSeed
{
    public class CompanySeed
    {
        public CompanyProfile NoStopTi()
        {

            List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){Name = "Instagram", Url ="marcusdias4243"},
                new SocialNetwork(){Name = "Facebook", Url ="https://www.facebook.com/marquinho.brasileiro.9"},
            };

            CompanyProfile company = new()
            {
                Id = 1,
                // Name = "No Stop Ti",
                CompanyAuthId = "!",
                BusinessProfileId = -1,
                CNPJ = "",
                Address = new()
                {

                    ZipCode = "30285100",
                    Street = "Arcos",
                    Number = "217",
                    District = "Vera Cruz",
                    City = "Belo Horizonte",
                    State = "MG",
                    Complement = null,
                    Registered = DateTime.Now,
                    Deleted = DateTime.MinValue,

                },
                Contact = new(
                     )
                {

                    Email = "contato@nostopti.com.br",
                    Site = "www.nostopti.com.br",
                    Cel = "31988598734",
                    Zap = "31982154642",
                    Landline = null,
                    SocialMedias = socialMedias,
                    Registered = DateTime.Now,
                    Deleted = DateTime.MinValue,

                }
            };
            return company;
        }

    }
}

