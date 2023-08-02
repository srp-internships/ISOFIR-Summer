using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Application.Interfaces.Services;

public interface IStudentService
{
    public Task<Result> CreateOrUpdateAsync(StudentRequestModel student);
    public Task<Result> DeleteAsync(int id);
    public Task<Result> GetByIdAsync(int id);
    public Task<Result> GetAllAsync();
    Task<Result> GetStudentPaysAsync(int studentId);
}