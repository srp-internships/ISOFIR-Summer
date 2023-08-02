using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Application.Services;

public class AuthService:IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result> GetTokenAsync(string login, string password)
    {
        var message = new HttpRequestMessage(HttpMethod.Post,
            "/api/auth/login?username=" + login + "&password=" + password);
        var response = await _httpClient.SendAsync(message);
        if (response.IsSuccessStatusCode)
        {
            return new OkResult<string>(await response.Content.ReadAsStringAsync());
        }
        return new ErrorResult("Invalid login or password");
    }

    public Task<Result> RegisterStudentAsync(StudentRequestModel student, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RegisterAgentAsync(Agent agent, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> IsAuthenticatedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetAgentFromTokenAsync(string token)
    {
        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            if (jwtSecurityTokenHandler.ReadToken(token) is not JwtSecurityToken jwtSecurityToken)
                return Task.FromResult<Result>(new ErrorResult(""));
            
            var claims = jwtSecurityToken.Claims as Claim[] ?? jwtSecurityToken.Claims.ToArray();
            
            var agent = new Agent
            {
                Id = int.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                UserName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                Role = claims.First(x=>x.Type==ClaimTypes.Role).Value
            };
            
            return Task.FromResult<Result>(new OkResult<Agent>(agent));
        }
        catch (Exception e)
        {
            return Task.FromResult<Result>(new ErrorResult(e));
        }
    }   
}