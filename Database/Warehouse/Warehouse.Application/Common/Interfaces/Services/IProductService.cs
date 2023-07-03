using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;

namespace Warehouse.Application.Common.Interfaces.Services;

public interface IProductService
{
    void CreateOrEdit(ProductRequestModel productDto);
    void Remove(int id);
    List<ProductResponseModel> GetAll();
}