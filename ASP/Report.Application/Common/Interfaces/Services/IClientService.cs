using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IClientService
{
    public Task<Result> CreateOrUpdateAsync(ClientRequestModel clientDto);
    public Task<Result> RemoveAsync(int id);
    public Task<Result> GetAllAsync();
    public Task<Result> GetClientsForSelectAsync();
}