using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using C21_PAP;
using C21_PAP.Shared;
using C21_PAP.ViewModels.TiposPropriedades;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("PropertyTypesAPI",
    client => client.BaseAddress = new Uri("https://web-worker-teste.henrique-melo.workers.dev")).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("PropertyTypesAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.DefaultScopes.Add("email");
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("read:agents", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "read:agents"));
});
builder.Services.AddMvvm();
builder.Services.AddScoped<CreateViewModel>();
builder.Services.AddScoped<EditViewModel>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();