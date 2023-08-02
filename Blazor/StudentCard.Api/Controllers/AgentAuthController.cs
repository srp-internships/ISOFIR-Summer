using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.ResponseModels;
using OkResult = StudentCard.Domain.OkResult;

namespace StudentCard.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AgentAuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AgentAuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string userName, string password)
    {
        var result = await _authService.Login(userName, password);
        if (result is OkResult<string> okResult)
        {
            return Ok(okResult.Result);
        }

        return StatusCode(400);
    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register(string userName, string password)
    {
        var result = await _authService.Register(new Agent {UserName = userName},password);
        if (result is OkResult)
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpPost("Register1")]
    public async Task<IActionResult> Register()
    {
        var result = await _authService.Register(new Agent {UserName = "Admin",Role = "Admin"}, "rtyVngL2TP@");
        if (result is OkResult)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet("GetAllAgents")]
    [Authorize(Roles=Roles.Admin)]
    public async Task<IActionResult> GetAllAgents()
    {
        var result = await _authService.GetAllAgents();
        if (result is OkResult<List<AgentResponseModel>> okResult)
        {
            return Ok(okResult.Result);
        }
        
        return StatusCode(400);
    }
}