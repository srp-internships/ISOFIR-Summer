namespace Report.Domain.Models;

public class Storage:BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public override string ToString() => Name;

    public IEnumerable<RestProduct>? RestProducts { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductsLogsFrom { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductsLogsTo { get; set; }
    
}