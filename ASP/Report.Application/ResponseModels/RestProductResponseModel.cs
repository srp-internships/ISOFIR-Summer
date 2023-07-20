namespace Report.Application.ResponseModels;

public class RestProductResponseModel
{
    public RestProductResponseModel()
    {
        Sum = Quantity * PriceUsd;
    }

    public int Id { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal PriceUsd { get; set; } = 0;
    public decimal PriceTjs { get; set; } = 0;
    public decimal Sum { get; }
    public string ProductName { get; init; } = "";
    public string StorageName { get; init; } = "";
    public string FirmName { get; init; } = "";

}