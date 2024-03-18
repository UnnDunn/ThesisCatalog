using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using ThesisCatalog.Client;
using ThesisCatalog.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:8081") });
builder.Services.AddScoped<ThesisHttpClientFactory>();
builder.Services.AddFluentUIComponents();

await builder.Build().RunAsync();