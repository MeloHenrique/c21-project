using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using C21_PAP;
using C21_PAP.Services;
using C21_PAP.Shared;
using C21_PAP.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PropertyTypesService>(client => client.BaseAddress = new Uri("https://property-types.henrique-melo.workers.dev")).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient<ContactOriginsService>(client => client.BaseAddress = new Uri("https://contact-origin.henrique-melo.workers.dev")).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

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
    options.AddPolicy("read:property_types", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "read:property_types"));
    options.AddPolicy("edit:property_types", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "edit:property_types"));
    options.AddPolicy("create:property_type", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "create:property_type")); 
    options.AddPolicy("read:contact_origins", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "read:contact_origins"));
    options.AddPolicy("edit:contact_origins", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "edit:contact_origins"));
    options.AddPolicy("create:contact_origin", policy => policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/policy", "create:contact_origin"));
});
builder.Services.AddMvvm();
builder.Services.AddScoped<C21_PAP.ViewModels.TiposPropriedades.CreateViewModel>();
builder.Services.AddScoped<C21_PAP.ViewModels.TiposPropriedades.EditViewModel>();
builder.Services.AddScoped<C21_PAP.ViewModels.TiposPropriedades.ViewViewModel>();
builder.Services.AddScoped<C21_PAP.ViewModels.ContactOrigins.ViewViewModel>();
builder.Services.AddScoped<C21_PAP.ViewModels.ContactOrigins.CreateViewModel>();
builder.Services.AddScoped<C21_PAP.ViewModels.ContactOrigins.EditViewModel>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();