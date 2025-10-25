using Application.Shared.Validators;

namespace Application.Helpers.Inject;

public interface IValidatorsInject
{
    IGenericValidators GenericValidators { get; }
}