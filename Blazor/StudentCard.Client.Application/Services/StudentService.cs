using System.Net.Http.Headers;
using System.Net.Http.Json;
using StudentCard.Client.Application.Common.Interfaces.Services;
using StudentCard.Domain;
using StudentCard.Domain.RequestModels;
using StudentCard.Domain.ResponseModels;

namespace StudentCard.Client.Application.Services;

public class StudentService: IStudentService
{
    private readonly HttpClient _httpClient;

    public StudentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result> GetAllAsync(string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var students = await _httpClient.GetFromJsonAsync<List<StudentResponseModel>>("/api/student/getAll");
            if (students != null) return new OkResult<List<StudentResponseModel>>(students);
            return new ErrorResult("error getting");
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }


    public Task<Result> GetAllAsync(string toString, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByIdAsync(int id, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> AddAsync(StudentRequestModel studentDto,string token)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("/api/student/create", studentDto);
            if (response.IsSuccessStatusCode)
            {
                return new OkResult();
            }
            return new ErrorResult(await response.Content.ReadAsStringAsync());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> UpdateAsync(StudentRequestModel studentDto, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(int id, string token)
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetByNameAsync(string name, string token)
    {
        throw new NotImplementedException();
    }
}