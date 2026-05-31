namespace Descargar_CFDIS.Helpers
{
    public static class ConditionalHelper
    {
        public static T Select<T>(bool condicion,T valorTrue,T valorFalse)
        {
            return condicion ? valorTrue : valorFalse;
        }
    }
}