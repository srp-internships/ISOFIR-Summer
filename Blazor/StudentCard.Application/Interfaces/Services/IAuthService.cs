using StudentCard.Domain;
using StudentCard.Domain.Models;

namespace StudentCard.Application.Interfaces.Services;

public interface IAuthService
{
    Task<Result> Register(Agent? user, string password);
    Task<bool> UserExists(string email);
    Task<Result> Login(string email, string password);
    Task<Result> ChangePassword(int userId, string newPassword);
    Result GetUserId();
    Result GetUserEmail();
    Task<Result> GetUserByEmail(string email);
    public Task<Result> GetAllAgents();
}