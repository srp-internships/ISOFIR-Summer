using WareHouse.Core.Models;

namespace Warehouse.Application.RequestModels;

public class ProductRequestModel:BaseModel
{
    public string Name { get; set; } = "";
}