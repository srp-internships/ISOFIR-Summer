using AutoMapper;
using OfficeOpenXml;
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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
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

    public async Task<Result> LoadFromExcelAsync(string path, bool mode)
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var book = new ExcelPackage(path);
            var sheet = book.Workbook.Worksheets.First();
            var i = 2;
            while(sheet.Cells["B" + i].Value + ""!="")
            {
                var category = await _categoryRepository.GetByNameAsync(sheet.Cells["C" + i].Value + "");
                if (category==null)
                    return new ErrorResult(new Exception(), $"Категория не найдена. Строка {i} >>> ");
                
                var product = new Product
                {
                    Name = sheet.Cells["B" + i].Value + "",
                    CategoryId = category.Id
                };
                await _productRepository.AddAsync(product);
                i++;
            }

            await _productRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}