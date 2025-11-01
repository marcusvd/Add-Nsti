
using System.Net;

using Application.Shared.Dtos;
using Application.Auth.Register.Services;
using Application.CompaniesServices.Dtos.Profile;
using Application.Auth.Register.Dtos;
using Application.Auth.Register.Dtos.FirstRegister;
using Application.Helpers.Tools.CpfCnpj;
using Application.Helpers.Tools.Cnpj;

namespace Application.Shared.Seed.EntitiesSeed
{
    public class AuthenticationSeed
    {
        private readonly IFirstRegisterBusinessServices _iFirstRegisterBusinessServices;
        public AuthenticationSeed(IFirstRegisterBusinessServices iFirstRegisterBusinessServices)
        {
            _iFirstRegisterBusinessServices = iFirstRegisterBusinessServices;
        }
        private CompanyProfileDto NoStopTi()
        {

            List<SocialNetworkDto> socialMedias = new(){
                new SocialNetworkDto(){Name = "Instagram", Url ="marcusdias4243"},
                new SocialNetworkDto(){Name = "Facebook", Url ="https://www.facebook.com/marquinho.brasileiro.9"},
            };

            var company = new CompanyProfileDto()
            {
                Id = 1,
                // Name = "No Stop Ti",
                CNPJ = "invalid",
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
                // Contact = ContactDto.Create(
                //     0,
                //     DateTime.MinValue,
                //     DateTime.UtcNow,
                //     "contato@nostopti.com.br",
                //     "www.nostopti.com.br",
                //     "31988598734",
                //     "31988598734",
                //     "3134832404",
                //     socialMedias
                // )
            };


            return company;
        }

        public async Task<HttpStatusCode> AddUser(ICpfCnpjGetDataServices cpfCnpjGetDataServices)
        {

            var user = new RegisterModelDto()
            {
                UserName = "Marcus Dias",
                Email = "marcusmvd@hotmail.com",
                CompanyName = "ADD-NSTI",
                Password = "123",
                ConfirmPassword = "123",
                CNPJ = "invalid"
            };

            // var result = await _iFirstRegisterBusinessServices.RegisterAsync(user, cpfCnpjGetDataServices);
            // var result = await _iFirstRegisterBusinessServices.RegisterAsync(user, new());

            if (true)
            // if (result.Authenticated)
                return HttpStatusCode.Created;
            // else
            //     return HttpStatusCode.BadRequest;

        }


    }
}