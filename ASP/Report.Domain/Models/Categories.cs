namespace Report.Domain.Models;

public class Category : BaseEntitiesModel
{
    public string Name { get; set; } = string.Empty;
    public List<Product>? Products { get; set; }
}