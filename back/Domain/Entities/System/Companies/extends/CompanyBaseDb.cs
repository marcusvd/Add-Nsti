using Domain.Entities.Shared;

namespace Domain.Entities.System.Companies.extends;

public class CompanyBaseDb: RootBaseDb
{
    public required string CNPJ { get; set; }
   
}