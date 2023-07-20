namespace Report.Domain.Models;

public class ClientCashLog:BaseNetWorth
{
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;

    public int CashBoxId { get; set; }
    public CashBox? CashBox { get; set; }
}