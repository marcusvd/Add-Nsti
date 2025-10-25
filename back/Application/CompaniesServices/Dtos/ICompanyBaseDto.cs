
using Application.Shared.Dtos;


namespace Application.CompaniesServices.Dtos;

public interface ICompanyBaseDto
{
    int Id { get; set; }
    string CNPJ { get; set; }
    DateTime Deleted { get; set; }
    DateTime Registered { get; set; }
}