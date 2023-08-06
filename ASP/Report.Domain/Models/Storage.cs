namespace Report.Domain.Models;

public class Storage : BaseEntitiesModel
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public IEnumerable<RestProduct>? RestProducts { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductsLogsFrom { get; set; }
    public IEnumerable<MoveProductLog>? MoveProductsLogsTo { get; set; }

    public override string ToString()
    {
        return Name;
    }
}