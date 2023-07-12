namespace Report.Application.RequestModels;

public class InvoiceRequestModel
{
    public int FirmId { get; set; }
    public DateTime DateTime { get; set; }=DateTime.Now;
    public int StorageId { get; set; }
    public int ProductId { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }
    public int Quantity { get; set; }
}