using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
// configure client for Pod Interactions
// builder.Services.AddHttpClient<IPodsDataService, PodsDataService>(
//         "PodsApi",
//         opt => opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001"))
//     .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
