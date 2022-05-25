using System.Net.Http.Json;
using C21_PAP.Models;
using C21_PAP.Models.Agents;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace C21_PAP.Services;

public class AgentService
{ 
    
    private readonly HttpClient _httpClient;
    
    public AgentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<PaginatedResponse<AgentModel>> GetAsync(TableRequestOptions options)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/get-all", options);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PaginatedResponse<AgentModel>>();

            if (result == null)
            {
                throw new ApplicationException("Error retrieving agents");
            }
            return result;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }
    
    public async Task<HttpResponseMessage> CreateAsync(CreateAgentModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/create", model);
            return response;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }

    public async Task<Response<string>> GetAgentTypeAsync()
    {
        
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/get-agent-types", "");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Response<string>>();

            if (result == null)
            {
                throw new ApplicationException("Error retrieving agents");
            }
            return result;
        }
        catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
            throw;
        }
    }
    
    public async Task<HttpResponseMessage> EditAsync(AgentModel model)
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
    
}