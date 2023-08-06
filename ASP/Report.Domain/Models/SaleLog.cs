namespace Report.Domain.Models;

public class SaleLog : BaseEntitiesModel
{
    public int RestProductId { get; set; }
    public RestProduct? RestProduct { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }

    public int Quantity { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
}