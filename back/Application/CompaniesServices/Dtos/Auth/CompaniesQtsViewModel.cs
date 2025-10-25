
namespace Application.CompaniesServices.Dtos.Auth;

public class CompaniesQtsViewModel
{
    public required int AmountCompanies { get; set; } = 0;
    public required List<int> CompaniesIds { get; set; } = [];
    public string? Name { get; set; }

}
