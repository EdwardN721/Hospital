using Hospital.API.Extensions;
using Hospital.Application.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Registrar dependencias
builder.Services.AddControllers();

// Agregar configuracion de excepciones y contexto de las peticiones web
builder.Services.AddApiServices();

// Agregar configuracion de Scalar
builder.Services.AddScalarConfiguration();

// Agregar servicios y validadores
builder.Services.AddApplicationServices();

// Servicios de Infraestructura
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// CONFIGURACIÓN DEL PIPELINE HTTP 

// Inyectamos el middleware global de excepciones
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Hospital API v1")
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();