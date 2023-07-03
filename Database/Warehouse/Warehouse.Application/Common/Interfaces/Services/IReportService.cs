using Warehouse.Application.ResponseModels;

namespace Warehouse.Application.Common.Interfaces.Services;

public interface IReportService
{
    List<ClientsHistoryResponseModel> GetClientsHistory();
    List<InvoiceHistoryResponseModel> GetInvoicesHistory();
}