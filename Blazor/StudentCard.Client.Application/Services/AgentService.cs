using System.Net.Http.Headers;
using System.Net.Http.Json;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Client.Application.Services;

public class AgentService:IAgentService
{
    private readonly HttpClient _httpClient;

    public AgentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result> GetAllAsync(string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var agents = await _httpClient.GetFromJsonAsync<List<AgentResponseModel>>("/api/auth/getAllAgents");
            if (agents != null) return new OkResult<List<AgentResponseModel>>(agents);
            return new ErrorResult("Error getting all agents");
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> GetByIdAsync(int id, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(AgentRequestModel agentDto, string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/auth/Register?username="+agentDto.UserName+"&password="+agentDto.Password);
            
            var response = await _httpClient.SendAsync(message);
            
            if (response.IsSuccessStatusCode) return new OkResult();
            
            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> UpdateAsync(AgentRequestModel agentDto, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(int id, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByNameAsync(string name, string token)
    {
        throw new NotImplementedException();
    }
}