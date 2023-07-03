using WareHouse.Core.Models;

namespace Warehouse.Application.RequestModels;

public class ClientRequestModel:BaseModel
{
    public string Name { get; set; } = "";
}