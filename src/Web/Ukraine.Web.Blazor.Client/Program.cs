using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Ukraine.Web.Blazor.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
	options.ProviderOptions.ClientId = "web-blazor";
	options.ProviderOptions.Authority = "http://localhost:7000";
	options.ProviderOptions.ResponseType = "code";

	options.ProviderOptions.DefaultScopes.Add("service-example-graphql-scope");
	options.ProviderOptions.DefaultScopes.Add("service-example-rest-scope");
});

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();