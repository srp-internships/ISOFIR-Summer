using StudentCard.Domain;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Client.Application.Common.Interfaces.Services;

public interface IStudentService
{
    public Task<Result> GetAllAsync(string token);
    public Task<Result> GetByIdAsync(int id,string token);
    public Task<Result> AddAsync(StudentRequestModel studentDto, string token);
    public Task<Result> UpdateAsync(StudentRequestModel studentDto, string token);
    public Task<Result> DeleteAsync(int id, string token); 
    public Task<Result> GetByNameAsync(string name, string token);
}