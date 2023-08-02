using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Application.Common.Interfaces.Services;

public interface IAuthService
{
    Task<Result> GetTokenAsync(string login, string password);
    Task<Result> RegisterStudentAsync(StudentRequestModel student, string token);
    Task<Result> RegisterAgentAsync(Agent agent, string token);
    Task<Result> IsAuthenticatedAsync();
    Task<Result> GetAgentFromTokenAsync(string token);
}