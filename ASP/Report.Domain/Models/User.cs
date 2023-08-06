namespace Report.Domain.Models;

public class User : BaseModel
{
    public string Name { get; set; } = "";
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    public IEnumerable<CashBox>? CashBoxes { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
    public IEnumerable<Client>? Clients { get; set; }
    public IEnumerable<ClientCashLog>? ClientCashLogs { get; set; }
    public IEnumerable<Firm>? Firms { get; set; }
    public IEnumerable<InvoiceLog>? InvoiceLogs { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductLogs { get; set; }
    public IEnumerable<Product>? Products { get; set; }
    public IEnumerable<Reason>? Reasons { get; set; }
    public IEnumerable<ReasonCashLog>? ReasonCashLogs { get; set; }
    public IEnumerable<RestProduct>? RestProducts { get; set; }
    public IEnumerable<SaleLog>? SaleLogs { get; set; }
    public IEnumerable<Storage>? Storages { get; set; }
}