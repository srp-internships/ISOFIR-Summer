namespace Report.Domain.Models;

public class ReasonCashLog : BaseEntitiesNetWorth
{
    public int ReasonId { get; set; }
    public Reason? Reason { get; set; }
    public int CashBoxId { get; set; }
    public CashBox? CashBox { get; set; }
    public DateTime DateTime { get; set; }
}