namespace Report.Domain.Models;

public class MoveProductLog:BaseModel
{
    public int FromStorageId { get; set; }
    public Storage? FromStorage { get; set; }

    public int ToStorageId { get; set; }
    public Storage? ToStorage { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}