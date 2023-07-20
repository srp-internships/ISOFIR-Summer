using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

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

    public async Task<Result> CreateOrUpdateAsync(ProductRequestModel productDto)
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
                await _productRepository.AddAsync(product);
            }

            await _productRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> RemoveAsync(int id)
    {
        try
        {
            await _productRepository.RemoveAsync(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e, "Не возможно удалить несуществующий объект");
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