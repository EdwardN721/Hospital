namespace Hospital.Domain.Exceptions;

public class AppValidationException : Exception
{
    // Diccionario que guardará: "NombreDelCampo" -> ["Error 1", "Error 2"]
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public AppValidationException(IReadOnlyDictionary<string, string[]> errors) 
        : base("Se encontraron uno o más errores de validación.")
    {
        Errors = errors;
    }
}