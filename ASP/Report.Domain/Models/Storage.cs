namespace Report.Core.Models;

public class Storage:BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public IEnumerable<RestProduct>? RestProducts { get; set; }
}