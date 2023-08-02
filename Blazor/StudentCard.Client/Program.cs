using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentCard.Client;
using StudentCard.Client.Application;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7174",UriKind.Absolute)});
builder.Services.AddApplication();

await builder.Build().RunAsync();