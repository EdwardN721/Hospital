using Microsoft.AspNetCore.Mvc;
using Hospital.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

namespace Hospital.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Ocurrió una excepción: {Message}", exception.Message);

        ProblemDetails problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        // Switch moderno (Pattern Matching) para atrapar el tipo de excepción
        switch (exception)
        {
            // --- Excepciones de Dominio / Negocio ---
            case NotFoundException notFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Recurso no encontrado";
                problemDetails.Detail = notFoundException.Message;
                break;

            case BusinessRuleException businessRuleException:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = "Regla de negocio invalidada";
                problemDetails.Detail = businessRuleException.Message;
                break;

            // --- Excepciones de Base de Datos (Entity Framework Core) ---
            case DbUpdateConcurrencyException concurrencyEx:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = "Conflicto de concurrencia";
                problemDetails.Detail = "El registro fue modificado por otro usuario mientras intentabas actualizarlo.";
                break;

            case DbUpdateException dbUpdateEx:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Error de actualización en base de datos";
                problemDetails.Detail = _env.IsDevelopment()
                    // En base de datos, solemos ocultar el error real en producción por seguridad
                    ? dbUpdateEx.InnerException?.Message ?? dbUpdateEx.Message
                    : "Ocurrió un problema al intentar guardar los cambios en la base de datos.";
                break;

            // --- Excepciones Generales ---
            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Error interno del servidor";
                problemDetails.Detail = _env.IsDevelopment()
                    ? exception.Message
                    : "Ocurrió un error inesperado. Contacte al administrador.";
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/problem+json"; // Estándar para ProblemDetails

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true; // Indicamos que la excepción fue manejada y no debe seguir propagándose
    }


}