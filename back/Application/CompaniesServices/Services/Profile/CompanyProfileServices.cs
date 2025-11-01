using Application.Exceptions;


using Domain.Entities.System.Companies;
using UnitOfWork.Persistence.Operations;
using Microsoft.EntityFrameworkCore;
using Application.CompaniesServices.Exceptions;
using Application.CompaniesServices.Extends;
using Application.CompaniesServices.Dtos.Mappers;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.CompaniesServices.Dtos.Profile;

namespace Application.CompaniesServices.Services.Profile;

public class CompanyProfileServices : CompanyServicesBase, ICompanyProfileServices
{
    private readonly IUnitOfWork _genericRepo;
    // private readonly IValidatorsInject _validatorsInject;
    public CompanyProfileServices(
                           IUnitOfWork genericRepo
                        //    IValidatorsInject validatorsInject
                           ) 
    {
        _genericRepo = genericRepo;
        // _validatorsInject = validatorsInject;
    }
    
    public CompanyProfile CompanyProfileEntityBuilder(PushCompanyDto dto)
    {
        return new()
        {
            CNPJ = dto.CNPJ,
            Address = dto.Address,
            Contact = dto.Contact
        };
    }
    public async Task<CompanyProfileDto> GetCompanyProfileFullAsync(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) throw new CompanyException(GlobalErrorsMessagesException.IsObjNull);

        return await _genericRepo.CompaniesProfile.GetByPredicate(
            x => x.CNPJ == cnpj && x.Deleted == DateTime.MinValue,
            add => add.Include(x => x.Address)
            .Include(x => x.Contact),
            selector => selector,
            null
            );
    }
    public async Task UpdateCompanyProfileSimple(CompanyProfileDto companyProfile)
    {
        var fromDb = await GetProfileById(companyProfile.Id);

        var companyProfileToUpdate = fromDb.ToUpdateSimple(companyProfile);

        _genericRepo.CompaniesProfile.Update(companyProfileToUpdate);

    }
    public async Task<CompanyProfile> GetProfileById(int id)
    {
        return await _genericRepo.CompaniesProfile.GetByPredicate(
                  x => x.Id == id && x.Deleted == DateTime.MinValue,
                  null,
                  selector => selector,
                  null
                  );
    }
}
