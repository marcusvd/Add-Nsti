using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Operations.Authentication.Register;
using Application.Services.Operations.Main.Companies.Dtos;
using Application.Services.Shared.Dtos;

namespace Application.Services.Shared.Seed.EntitiesSeed
{
    public class AuthenticationSeed
    {
        private readonly IRegisterServices _iRegisterServices;
        public AuthenticationSeed(IRegisterServices iRegisterServices)
        {
            _iRegisterServices = iRegisterServices;
        }
        private CompanyDto NoStopTi()
        {

            List<SocialNetworkDto> socialMedias = new(){
                new SocialNetworkDto(){Name = "Instagram", Url ="marcusdias4243"},
                new SocialNetworkDto(){Name = "Facebook", Url ="https://www.facebook.com/marquinho.brasileiro.9"},
            };

            var company = new CompanyDto()
            {
                Id = 1,
                Name = "No Stop Ti",
                Address = new()
                {
                    ZipCode = "30285100",
                    Street = "Arcos",
                    Number = "217",
                    District = "Vera Cruz",
                    City = "Belo Horizonte",
                    State = "MG",
                    Complement = ""
                },
                Contact = new()
                {
                    Email = "contato@nostopti.com.br",
                    Site = "www.nostopti.com.br",
                    Cel = "31988598734",
                    Zap = "31988598734",
                    Landline = "3134832404",
                    SocialMedias = socialMedias

                }
            };


            return company;
        }

        public async Task<HttpStatusCode> AddUser()
        {

            var user = new MyUserDto()
            {
                Id = 1,
                Company = NoStopTi(),
                UserName = "Marcus Dias",
                Email = "marcusmvd@hotmail.com",
                Password = "123",
                ConfirmPassword = "123"
            };


            var result = await _iRegisterServices.Register(user);
            if (result.Authenticated)
                return HttpStatusCode.Created;
            else
                return HttpStatusCode.BadRequest;

        }


    }
}