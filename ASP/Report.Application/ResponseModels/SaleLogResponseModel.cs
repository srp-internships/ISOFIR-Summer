namespace Report.Application.ResponseModels;

public class SaleLogResponseModel : BaseResponseModel
{
    public string Client { get; set; } = "";
    public string Storage { get; set; } = "";
    public string Product { get; set; } = "";
    public int Quantity { get; set; }
    public decimal SalePriceUsd { get; set; }
    public decimal SalePriceTjs { get; set; }
    public DateTime DateTime { get; set; }
}