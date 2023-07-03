namespace WareHouse.Core.Models;

public class Product:BaseModel
{
    public string Name { get; set; }
    public IEnumerable<Rest> Rests { get; set; } = null!;
}