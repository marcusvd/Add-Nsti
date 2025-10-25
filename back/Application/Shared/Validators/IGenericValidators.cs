namespace Application.Shared.Validators;

public interface IGenericValidators
{

    void IsObjNull<T>(T obj) where T : class;
    List<T> EmptyListBuilder<T>(List<T> obj) where T : class;
    bool Validate<T>(T dtoId, T paramId, string messageException);
   
    // Object ReplaceNullObj<T>();

}

