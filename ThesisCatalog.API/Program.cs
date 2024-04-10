using Microsoft.EntityFrameworkCore;
using ThesisCatalog.API.Data;
using ThesisCatalog.API.Data.Helpers;
using ThesisCatalog.API.Services;
using ThesisCatalog.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ThesisCatalogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThesisCatalogDbContext"));
    options.AddInterceptors(new SetSearchTextInterceptor());
}, ServiceLifetime.Singleton, ServiceLifetime.Singleton);

builder.Services.AddSingleton<CatalogService>();

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.TypeInfoResolverChain.Add(ThesisCatalogJsonSerializerContext.Default));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("https://localhost:7043",
                "http://localhost:7043",
                "https://*.unndunn.com",
                "https://*.unndunn.net")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();
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
