using Report.Application.RequestModels;
using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IReasonCashBoxService
{
    public Task<Result> PayWithDraw(ReasonCashBoxRequestModel clientCash);

    Task<Result> GetAllHistoryAsync(int userId);
}