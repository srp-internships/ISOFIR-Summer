using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Common.Interfaces.Services;

public interface IClientService
{
    void CreateOrEdit(ClientRequestModel clientDto);
    void Remove(int id);
    List<ClientResponseModel> GetAll();
}