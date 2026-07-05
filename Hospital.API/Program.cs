using Hospital.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Servicios de la API
builder.Services.AddApiServices();

// Servicios de Infraestructura
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Agregar Swagger


var app = builder.Build();

// CONFIGURACIÓN DEL PIPELINE HTTP 

// Inyectamos el middleware global de excepciones ANTES que cualquier otra cosa
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();