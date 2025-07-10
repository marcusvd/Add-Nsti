

using System.Linq;
using Domain.Entities.Companies;
using Repository.Data.Context;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities.Shared;
using System;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Shared.Seed.EntitiesSeed
{
    public class CompanySeed
    {
        public Company NoStopTi()
        {

            List<SocialNetwork> socialMedias = new(){
                new SocialNetwork(){Name = "Instagram", Url ="marcusdias4243"},
                new SocialNetwork(){Name = "Facebook", Url ="https://www.facebook.com/marquinho.brasileiro.9"},
            };

            Company company = new Company(1, "No Stop Ti");
            company.Address = new()
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

            };
            company.Contact = new(
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

            };

            return company;
        }

    }
}

