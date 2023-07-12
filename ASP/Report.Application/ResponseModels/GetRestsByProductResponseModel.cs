
namespace Report.Application.ResponseModels;

public class GetRestsByProductResponseModel
{
    public int RestId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }
    public string StorageName { get; set; } = "";
}