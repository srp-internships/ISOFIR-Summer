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
[Route("/api/student")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [Authorize]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _studentService.GetAllAsync();
        if (result is OkResult<List<StudentResponseModel>> okResult)
        {
            return Ok(okResult.Result);
        }

        return StatusCode(500);
    }

    [Authorize]
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromBody] int id)
    {
        var result = await _studentService.GetByIdAsync(id);
        if (result is OkResult<Student> okResult)
        {
            return Ok(okResult.Result);
        }
        return StatusCode(500);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] StudentRequestModel student)
    {
        student.Id = 0;
        var result = await _studentService.CreateOrUpdateAsync(student);
        return result is OkResult ? Ok() : StatusCode(400);
    }
    
    
    [Authorize]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(StudentRequestModel student)
    {
        var result = await _studentService.CreateOrUpdateAsync(student);
        return result is OkResult ? Ok() : StatusCode(400);
    }

    [Authorize]
    [HttpGet("GetStudentPays")]
    public async Task<IActionResult> GetStudentPays(int studentId)
    {
        var result = await _studentService.GetStudentPaysAsync(studentId);
        if (result is OkResult<List<PayResponseModel>> okResult)
        {
            return Ok(okResult.Result);
        }
        return StatusCode(500);
    }
}