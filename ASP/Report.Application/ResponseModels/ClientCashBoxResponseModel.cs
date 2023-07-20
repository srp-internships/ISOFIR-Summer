using Report.Domain.Models;

namespace Report.Application.ResponseModels;

public class ClientCashBoxResponseModel:BaseNetWorth
{
    public string DateTime { get; set; } = "";
    public string ClientName { get; set; } = "";
    public string CashBoxName { get; set; } = "";
}