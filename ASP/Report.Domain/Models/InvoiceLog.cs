namespace Report.Core.Models;

public class InvoiceLog:BaseModel
{
    public int RestProductId { get; set; }
    public RestProduct? RestProduct { get; set; }

    public int FirmId { get; set; }
    public Firm? Firm { get; set; }

    public decimal PriceUsd { get; set; }
    public decimal PriceTjs { get; set; }
    
    public int Quantity { get; set; }
    public DateTime DateTime { get; set; }=DateTime.Now;

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}