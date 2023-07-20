using Report.Domain.Models;

namespace Report.Application.RequestModels;

public class ClientCashBoxRequestModel:BaseNetWorth
{
    public int ClientId { get; set; }
    public DateTime Type { get; set; }
    public int CashBoxId { get; set; }
}