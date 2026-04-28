using Blazored.LocalStorage;
using Contentful.AspNetCore;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using YogaRetreat;
using YogaRetreat.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// MudBlazor
builder.Services.AddMudServices();

// Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Cache service
builder.Services.AddScoped<ICacheService, CacheService>();

// Contentful
builder.Services.AddContentful(builder.Configuration);

// Contentful service
builder.Services.AddScoped<IContentfulService, ContentfulService>();

await builder.Build().RunAsync();
