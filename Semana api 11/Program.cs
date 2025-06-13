using Microsoft.EntityFrameworkCore;
using Semana_api_11.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicación
builder.Services.AddControllers();

// Agregar soporte para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar tu contexto de base de datos
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
