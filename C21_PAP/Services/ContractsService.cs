using System.Net.Http.Json; 
using C21_PAP.Models.PostalCodeInfo;
using C21_PAP.Models.Process;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace C21_PAP.Services;

public class ContractsService
{
    private readonly HttpClient _httpClient;
    
    public ContractsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<PostalCodeInfo> GetPostalCodeInfo(int postalCode)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/postal-code/info", postalCode);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PostalCodeInfo>();
            if (result == null)
            {
                throw new ApplicationException("Error retrieving postal code info!");
            }
            return result;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }

    public async Task<HttpResponseMessage> Create(CreateProcessModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/create/", model);
            return response;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }
}