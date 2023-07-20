namespace Report.Domain.Models;

public class Firm:BaseNetWorth
{
    public string Name { get; set; }=string.Empty;

    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
}