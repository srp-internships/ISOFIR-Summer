using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using Report.Core.Models;

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

    public async Task<Result> CreateOrUpdate(CategoryRequestModel categoryDto)
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
                _categoryRepository.Add(category);

            _categoryRepository.SaveChanges();
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
            _categoryRepository.Remove(id);
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