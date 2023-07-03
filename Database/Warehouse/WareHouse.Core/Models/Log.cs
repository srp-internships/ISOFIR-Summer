namespace WareHouse.Core.Models;

public class BaseBusinessProcessLog : BaseModel
{
    public DateTime DateTime { get; set; }
    public int Quantity { get; set; }

    public int RestId { get; set; }
    public Rest Rest { get; set; } = null!;
    public decimal Price { get; set; }

}

public class InvoiceLog:BaseBusinessProcessLog{ }

public class SaleLog : BaseBusinessProcessLog
{
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}