using Application.Helpers.Inject;
using Application.Shared.Validators;

namespace Application.Helpers.ServicesLauncher;

public class ValidatorsInject : IValidatorsInject
{
   public ValidatorsInject() {}

    #region 
    private GenericValidators _genericvalidator = new GenericValidators();
    public IGenericValidators GenericValidators
    {
        get
        {
            return _genericvalidator;
        }
    }
    #endregion
}
