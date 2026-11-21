using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebClient;
using WebClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<ICountriesService, CountriesService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "http://localhost:5176");
});

builder.Services.AddHttpClient<IRecordsService, RecordsService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "http://localhost:5176");
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
