using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCard.Application.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;
using OkResult = StudentCard.Domain.OkResult;

namespace StudentCard.Api.Controllers;

[ApiController]
[Route("/api/pay")]
public class PayController:ControllerBase
{
    private readonly IPayService _payService;

    public PayController(IPayService payService)
    {
        _payService = payService;
    }

    [Authorize]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _payService.GetAllAsync();
        if (result is OkResult<List<PayResponseModel>> okResult)
        {
            return Ok(okResult.Result);
        }
        return StatusCode(500);
    }

    [Authorize]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromBody] int id)
    {
        var result = await _payService.GetByIdAsync(id);
        if (result is OkResult<PayResponseModel> okResult)
        {
            return Ok(okResult.Result);
        }
        return StatusCode(500);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody]PayRequestModel pay)
    {
        pay.Id = 0;
        var result = await _payService.CreateOrUpdateAsync(pay);
        return result is OkResult ? Ok() : StatusCode(400);
    }
    
    
    [Authorize]
    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody]PayRequestModel pay)
    {
        var result = await _payService.CreateOrUpdateAsync(pay);
        return result is OkResult ? Ok() : StatusCode(400);
    }
}