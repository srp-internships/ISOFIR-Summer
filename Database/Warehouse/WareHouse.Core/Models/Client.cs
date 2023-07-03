namespace WareHouse.Core.Models;

public class Client:BaseModel
{
    public string Name { get; set; }
    public IEnumerable<SaleLog> SaleLogs { get; set; }
}