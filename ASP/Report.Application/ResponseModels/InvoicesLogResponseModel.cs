namespace Report.Application.ResponseModels;

public class InvoicesLogResponseModel:BaseResponseModel
{
    public int Quantity { get; set; }
    public string FirmName { get; set; } = string.Empty;
    public string DateTime { get; set; } = string.Empty;
    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }
    public string ProductName { get; set; } = string.Empty;
    
}