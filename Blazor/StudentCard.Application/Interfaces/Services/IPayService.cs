using StudentCard.Domain;
using StudentCard.Domain.Models;
using StudentCard.Domain.RequestModels;

namespace StudentCard.Application.Interfaces.Services;

public interface IPayService
{
    public Task<Result> CreateOrUpdateAsync(PayRequestModel pay);
    public Task<Result> DeleteAsync(int id);
    public Task<Result> GetByIdAsync(int id);
    public Task<Result> GetAllAsync();
}