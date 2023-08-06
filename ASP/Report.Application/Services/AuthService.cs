using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class AuthService : IAuthService
{
    internal static string Token = "";
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> GetUserAsync(string login, string password)
    {
        try
        {
            var user = await _userRepository.GetByLoginAsync(login);

            if (user == null) return new ErrorResult("Wrong username");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return new ErrorResult("Wrong password");

            return new OkResult<User>(user);
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> ChangePasswordAsync(int userId, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return new ErrorResult("User not found");

            CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> RegisterAsync(string login, string password)
    {
        try
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user != null) return new ErrorResult("User already exists");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user = new User
            {
                Login = login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac
            .ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash =
            hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(Token));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}