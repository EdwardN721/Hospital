namespace Hospital.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key) 
        : base($"El recurso '{entityName}' con la clave ({key}) no fue encontrado.")
    {  }

      public NotFoundException(string? message) : base(message)
    { }
}