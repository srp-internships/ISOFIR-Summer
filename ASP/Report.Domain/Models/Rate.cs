namespace Report.Domain.Models;

public class Rate : BaseEntitiesModel
{
    public DateTime DateTime { get; set; } = DateTime.Now;
    public decimal OneDollarIs { get; set; }
}