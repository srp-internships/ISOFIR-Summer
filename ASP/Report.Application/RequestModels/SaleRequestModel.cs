namespace Report.Application.RequestModels;

public class SaleRequestModel
{
    public int RestId { get; set; }
    public int Quantity { get; set; }
    public decimal SalePriceTjs { get; set; }
    public decimal SalePriceUsd { get; set; }
    public DateTime DateTime { get; set; }
    public int ClientId { get; set; }
}