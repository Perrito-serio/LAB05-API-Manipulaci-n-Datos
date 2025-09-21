using LAB05_MunozHerrera.Application;
using Microsoft.EntityFrameworkCore;
using LAB05_MunozHerrera.Data;
using LAB05_MunozHerrera.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Obtener la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 3. REGISTRAR EL UNIT OF WORK (¡Esto reemplaza a los repositorios individuales!)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 4. Agregar servicios para los controladores
builder.Services.AddControllers(); 

// 5. Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();