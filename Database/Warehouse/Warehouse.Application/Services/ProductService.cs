using AutoMapper;
using Warehouse.Application.Common.Interfaces.Repositories;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.RequestModels;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Services;

public class ProductService:IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    
    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public void CreateOrEdit(ProductRequestModel productDto)
    {
        var product = _mapper.Map<ProductRequestModel, Product>(productDto);
        if (product!=null)
        {
            var old = _productRepository.GetIEnumerable().FirstOrDefault(s => s.Id == product.Id);
            if (old!=null)
            {
                _mapper.Map(productDto, old);
            }
            else
            { 
                _productRepository.Add(product);
            }

            _productRepository.Save();
        }
    }

    public void Remove(int id)
    { 
        _productRepository.Remove(id);
        _productRepository.Save();
    }

    public List<ProductResponseModel> GetAll()
    {
        return _productRepository.GetIEnumerable().Select(s=>_mapper.Map<Product,ProductResponseModel>(s)).ToList();
    }
}