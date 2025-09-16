using Microsoft.EntityFrameworkCore;
using LAB05_MunozHerrera.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


// 1. Obtener la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Esto le dice a la aplicación cómo crear instancias de AppDbContext
// y conectarlas a PostgreSQL.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


/////implementacion///

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

////fin implementation///

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    
    ////Implementacion Swagger////
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    ///Fin de la implementacion///
    
}

app.UseHttpsRedirection();

//Se agrega//
app.MapControllers();
//-------//

/* ESTO ES UN EJEMPLO QUE NOS DA EL PROGRAM.CS
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/

app.Run();