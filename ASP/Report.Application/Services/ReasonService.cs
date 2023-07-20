using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ReasonService : IReasonService
{
    private readonly IReasonRepository _reasonReasonRepository;
    private readonly IMapper _mapper;

    public ReasonService(IReasonRepository reasonRepository, IMapper mapper)
    {
        _reasonReasonRepository = reasonRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(ReasonRequestModel reasonDto)
    {
        try
        {
            var category = _mapper.Map<ReasonRequestModel, Reason>(reasonDto);
            if (category == null)
                return new ErrorResult(new Exception(), "Невозможно обработать ваши данные");
            

            var old = await _reasonReasonRepository.GetByIdAsync(category.Id);
            if (old!=null)
                old.Name = category.Name;
            else
                await _reasonReasonRepository.AddAsync(category);

            await _reasonReasonRepository.SaveChangesAsync();
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
            await _reasonReasonRepository.RemoveAsync(id);
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
            var result = await _reasonReasonRepository.GetAllAsync();
            return new OkResult<List<ReasonResponseModel>>(result
                .Select(s => _mapper.Map<Reason, ReasonResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}