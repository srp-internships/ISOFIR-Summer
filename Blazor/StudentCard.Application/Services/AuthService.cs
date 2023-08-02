using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentCard.Application.Infrastructure.DataBase;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.ResponseModels;
using OkResult = StudentCard.Domain.OkResult;


namespace StudentCard.Application.Services;

public class AuthService : IAuthService
{
    internal static string Token = "";
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(DataContext context,
        IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }


    public async Task<Result> Register(Agent? user, string password)
    {
        try
        {
            if (user != null && await UserExists(user.UserName))
            {
                return new ErrorResult("User already registered");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Agents.Add(user);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<bool> UserExists(string email)
    {
        try
        {
            return await _context.Agents.AnyAsync(user => user.UserName.ToLower()
                .Equals(email.ToLower()));
        }
        catch (Exception e)
        {
            return true;
        }
    }

    public async Task<Result> Login(string email, string password)
    {
        try
        {
            var user = await _context.Agents
                .FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                return new ErrorResult("UserName not found");
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult("Wrong password");
            }

            return new OkResult<string>(CreateToken(user));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> ChangePassword(int userId, string newPassword)
    {
        try
        {
            var user = await _context.Agents.FindAsync(userId);
            if (user == null)
            {
                return new ErrorResult("User not found");
            }

            CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }


    public Result GetUserId()
    {
        try
        {
            return new OkResult<int>(
                int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Result GetUserEmail()
    {
        try
        {
            return new OkResult<string?>(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetUserByEmail(string email)
    {
        try
        {
            return new OkResult<Agent?>(
                await _context.Agents.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(email.ToLower())));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac
            .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(Agent user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(Token));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
    public async Task<Result> GetAllAgents()
    {
        try
        {
            var agents = await _context.Agents
                .Include(s => s.Pays)
                .Select(s => _mapper.Map<AgentResponseModel>(s)).ToListAsync();
            return new OkResult<List<AgentResponseModel>>(agents);
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}