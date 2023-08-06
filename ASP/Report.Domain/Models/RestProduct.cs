namespace Report.Domain.Models;

public class RestProduct : BaseEntitiesModel
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int StorageId { get; set; }
    public Storage? Storage { get; set; }

    public int Quantity { get; set; }
    public decimal InvoicePriceTjs { get; set; }
    public decimal InvoicePriceUsd { get; set; }
    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
}