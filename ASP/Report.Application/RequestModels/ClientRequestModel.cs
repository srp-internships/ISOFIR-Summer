namespace Report.Application.RequestModels;

public class ClientRequestModel:BaseRequestModel
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal CashTjs { get; set; }
}