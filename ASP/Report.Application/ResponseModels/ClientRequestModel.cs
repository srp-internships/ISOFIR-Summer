using Report.Application.RequestModels;

namespace Report.Application.ResponseModels;

public class ClientResponseModel:BaseRequestModel
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal CashTjs { get; set; }
    public decimal Income { get; set; }
    public decimal Sales { get; set; }
}