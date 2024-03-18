using Microsoft.EntityFrameworkCore;
using ThesisCatalog.API.Data;
using ThesisCatalog.API.Services;
using ThesisCatalog.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ThesisCatalogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThesisCatalogDbContext"));
}, ServiceLifetime.Singleton, ServiceLifetime.Singleton);

builder.Services.AddSingleton<CatalogService>();

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.TypeInfoResolverChain.Add(ThesisCatalogJsonSerializerContext.Default));
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
    .WithName("GetWeatherForecast")
    .WithOpenApi();

CreateDbIfNotExists(app).Wait();
app.Run();


static async Task CreateDbIfNotExists(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Creating the database");
    try
    {
        var context = services.GetRequiredService<ThesisCatalogDbContext>();
        await context.Database.MigrateAsync();

        var catalogService = services.GetRequiredService<CatalogService>();
        await catalogService.SeedInitialData();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "A problem occurred updating the database");
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
