using Report.Domain.ActionResults;

namespace Report.Application.Common.Interfaces.Services;

public interface IReportService
{
    Task<Result> GetInvoicesLogAsync(int userId);
    Task<Result> GetClientHistoryAsync(int clientId, int userId);
    Task<Result> GetStorageRestAsync(int storageId);
    Task<Result> GetToDayTurnOverAsync(int userId);
    Task<Result> GetToDayClearAsync(int userId);
    Task<Result> GetToDaySalesQuantityAsync(int userId);
    // Task<Result> GetToDayPaysAsync(int userId);
    Task<Result> ExportRestsToExcelAsync(int userId,string path);
    public Task<Result> ExportDebtsToExcelAsync(int userId, string path);
}