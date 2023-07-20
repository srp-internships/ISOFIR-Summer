using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IClientCashBoxService
{
    public Task<Result> PayWithDraw(ClientCashBoxRequestModel clientCash);
    public Task<Result> GetAllHistoryAsync();
}