using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

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

    public async Task<Result> CreateOrUpdateAsync(StorageRequestModel storageDto)
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
                await _storageRepository.AddAsync(storage);
            }

            await _storageRepository.SaveChangesAsync();
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
            await _storageRepository.RemoveAsync(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e,"Не возможно удалить несуществующий объект");
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

    public async Task<Result> GetStorageRestsAsync(int storageId)
    {
        try
        {
            var rests = await _storageRepository.GetStorageRestsAsync(storageId);
            return new OkResult<List<RestProductResponseModel>>(rests
                .Select(s => _mapper.Map<RestProduct, RestProductResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}