using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IReportService
{
    Task<Result> GetInvoicesLogAsync();
    Task<Result> GetClientHistoryAsync(int clientId);
}