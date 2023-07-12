namespace Report.Core.Models;

public class SaleLog:BaseModel
{
    public int RestId { get; set; }
    public RestProduct? RestProduct { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }
    
    public int Quantity { get; set; }
    public DateTime DateTime { get; set; }=DateTime.Now;
}