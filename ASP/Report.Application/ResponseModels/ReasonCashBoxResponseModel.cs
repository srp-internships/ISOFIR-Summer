using Report.Domain.Models;

namespace Report.Application.ResponseModels;

public class ReasonCashBoxResponseModel:BaseNetWorth
{
    public string DateTime { get; set; } = "";
    public string ReasonName { get; set; } = "";
    public string CashBoxName { get; set; } = "";
}