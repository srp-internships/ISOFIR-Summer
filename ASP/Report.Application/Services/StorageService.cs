using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Core.ActionResults;
using Report.Core.Models;

namespace Report.Application.Services;

public class StorageService:IStorageService
{
    private readonly IMapper _mapper;
    private readonly IStorageRepository _storageRepository;

    public StorageService(IStorageRepository storageRepository, IMapper mapper)
    {
        _storageRepository = storageRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdate(StorageRequestModel storageDto)
    {
        try
        {
            var storage = _mapper.Map<StorageRequestModel, Storage>(storageDto);
            if (storage == null)
            {
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            }

            var old = await _storageRepository.GetByIdAsync(storage.Id);
            if (old!=null)
            {
                old.Name = storage.Name;
            }
            else
            {
                _storageRepository.Add(storage);
            }

            _storageRepository.SaveChanges();
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
            _storageRepository.Remove(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e,"Не возможно удалить несуществующий обект");
        }
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {
            var result =  await _storageRepository.GetAllAsync();
            return new OkResult<List<StorageResponseModel>>(result.Select(s=>_mapper.Map<Storage,StorageResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}