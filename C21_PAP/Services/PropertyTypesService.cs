using System.Net.Http.Json;
using C21_PAP.Models;
using C21_PAP.Models.TiposPropriedades;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace C21_PAP.Services;

public class PropertyTypesService
{
    private readonly HttpClient _httpClient;
    
    public PropertyTypesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PaginatedResponse<TipoPropriedadeModel>> GetAsync(RequestOptions options)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/get-all/", options);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PaginatedResponse<TipoPropriedadeModel>>();

            if (result == null)
            {
                throw new ApplicationException("Error retrieving property types");
            }
            return result;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }

    public async Task<HttpResponseMessage> EditAsync(TipoPropriedadeModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/edit", model);   
            return response;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }
    
    public async Task<HttpResponseMessage> CreateAsync(CreatePropertyTypeModel model)
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