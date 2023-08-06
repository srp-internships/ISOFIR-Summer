namespace Report.Domain.Models;

public class Product : BaseEntitiesModel
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public IEnumerable<RestProduct>? RestProducts { get; set; }
    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductsLogs { get; set; }

    public override string ToString()
    {
        return Name;
    }
}