namespace Application.CompaniesServices.Exceptions;

public class CompanyException : Exception
{
    public CompanyException(string message) : base(message) { }
}