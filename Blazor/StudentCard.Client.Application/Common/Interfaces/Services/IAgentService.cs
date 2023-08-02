using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Application.Common.Interfaces.Services;

public interface IAgentService
{
    public Task<Result> GetAllAsync(string token);
    public Task<Result> GetByIdAsync(int id,string token);
    public Task<Result> AddAsync(AgentRequestModel agentDto, string token);
    public Task<Result> UpdateAsync(AgentRequestModel agentDto, string token);
    public Task<Result> DeleteAsync(int id, string token); 
    public Task<Result> GetByNameAsync(string name, string token);
}