namespace Authentication.Helpers;

public interface IGenericValidatorServices
{

    void IsObjNull<T>(T obj) where T : class;
   
    bool Validate<T>(T dtoId, T paramId, string messageException);
   
    Object ReplaceNullObj<T>();

}

