namespace Application.Exceptions
{

    public static class GlobalErrorsMessagesException
    {
        public static readonly string IdIsNull = "100.0|Id era nulo.";
        public static readonly string EntityFromIdIsNull = "100.1|Entidade by id era nulo.";
        public static readonly string IdIsDifferentFromEntityUpdate = "100.2|ID passed cannot be different from entity passed by body.";
        public static readonly string ObjIsNull = "100.3|O objeto era nulo.";
        public static readonly string UnknownError = "100.4|O Erro desconhecido.";
        public static readonly string BusinessRulesViolation = "100.5|Regra de negócio violada.";

    }
}














