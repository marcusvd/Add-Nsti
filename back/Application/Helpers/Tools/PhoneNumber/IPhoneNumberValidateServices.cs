namespace Application.Helpers.Tools.ZipCode;

using System.Threading.Tasks;
using Shared.Dtos;
public interface IPhoneNumberValidateServices
{
        // Task<string> PhoneNumberValidate(string numbers);
        string IsMobile(string numero);
}
