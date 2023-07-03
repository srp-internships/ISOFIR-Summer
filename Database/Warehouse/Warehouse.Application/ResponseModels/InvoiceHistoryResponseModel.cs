namespace Warehouse.Application.ResponseModels;

public class InvoiceHistoryResponseModel
{
    public string ProductName { get; set; } = "";
    public string DateTime { get; set; } = "";
    public decimal Price { get; set; }
}