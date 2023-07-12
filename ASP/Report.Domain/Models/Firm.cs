namespace Report.Core.Models;

public class Firm:BaseNetWorth
{
    public string Name { get; set; }=string.Empty;
    public decimal NetWorth { get; set; }

    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
}