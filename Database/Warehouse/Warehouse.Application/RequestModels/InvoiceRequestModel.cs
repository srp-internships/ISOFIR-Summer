namespace Warehouse.Application.RequestModels;

public class InvoiceRequestModel
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
}