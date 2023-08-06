using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Common.Interfaces.Services;

public interface IAuthService
{
    Task<Result> GetUserAsync(string login, string password);
    Task<Result> ChangePasswordAsync(int userId, string newPassword);
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string CreateToken(User user);
    public Task<Result> RegisterAsync(string login, string password);
}