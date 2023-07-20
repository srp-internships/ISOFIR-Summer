using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(CategoryRequestModel categoryDto)
    {
        try
        {
            var category = _mapper.Map<CategoryRequestModel, Category>(categoryDto);
            if (category == null)
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            

            var old = await _categoryRepository.GetByIdAsync(category.Id);
            if (old!=null)
                old.Name = category.Name;
            else
                await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveChangesAsync();
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
            await _categoryRepository.RemoveAsync(id);
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
            var result = await _categoryRepository.GetAllAsync();
            return new OkResult<List<CategoryResponseModel>>(result
                .Select(s => _mapper.Map<Category, CategoryResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}