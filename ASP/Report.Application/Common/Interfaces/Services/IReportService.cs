using Report.Core.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IReportService
{
    Task<Result> GetInvoicesLogAsync();
    Task<Result> GetClientHistory(int clientId);
}