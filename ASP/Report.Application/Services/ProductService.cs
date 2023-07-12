using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using Report.Core.Models;

namespace Report.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdate(ProductRequestModel productDto)
    {
        try
        {
            var product = _mapper.Map<ProductRequestModel, Product>(productDto);
            if (product == null)
            {
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            }

            var old = await _productRepository.GetByIdAsync(product.Id);
            if (old != null)
            {
                old.Name = product.Name;
                old.CategoryId = product.CategoryId;
            }
            else
            {
                _productRepository.Add(product);
            }

            _productRepository.SaveChanges();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Result Remove(int id)
    {
        try
        {
            _productRepository.Remove(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e, "Не возможно удалить несуществующий обект");
        }
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {
            var result = await _productRepository.GetAllAsync();
            return new OkResult<List<ProductResponseModel>>(result
                .Select(s => _mapper.Map<Product, ProductResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}