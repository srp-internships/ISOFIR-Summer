namespace WareHouse.Core.Models;

public class Rest:BaseModel
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public IEnumerable<InvoiceLog> InvoiceLogs { get; set; } = null!;
    public IEnumerable<SaleLog> SaleLogs { get; set; } = null!;
}