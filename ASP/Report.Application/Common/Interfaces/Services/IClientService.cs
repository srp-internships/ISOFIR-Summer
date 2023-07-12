using Report.Application.RequestModels;
using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IClientService
{
    public Task<Result> CreateOrUpdate(ClientRequestModel clientDto);
    public Result Remove(int id);
    public Task<Result> GetAllAsync();
    public Task<Result> GetClientsForSelectAsync();
}