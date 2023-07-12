namespace Report.Core.Models;

public class Product:BaseModel
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public override string ToString() => Name;

    public IEnumerable<RestProduct>? RestProducts { get; set; }
    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
}