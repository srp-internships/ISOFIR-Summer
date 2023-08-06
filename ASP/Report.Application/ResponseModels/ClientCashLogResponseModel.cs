namespace Report.Application.ResponseModels;

public class ClientCashLogResponseModel : BaseResponseModel
{
    public string DateTime { get; set; } = "";
    public string CashBox { get; set; } = "";
    public decimal CashUsd { get; set; }
    public decimal CashTjs { get; set; }
}