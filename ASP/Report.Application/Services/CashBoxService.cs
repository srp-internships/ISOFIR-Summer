using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class CashBoxService : ICashBoxService
{
    private readonly ICashBoxRepository _cashBoxRepository;
    private readonly IMapper _mapper;

    public CashBoxService(ICashBoxRepository cashBoxRepository, IMapper mapper)
    {
        _cashBoxRepository = cashBoxRepository;
        _mapper = mapper;
    }

    public async Task<Result> CreateOrUpdateAsync(CashBoxRequestModel cashBoxDto)
    {
        try
        {
            var old = await _cashBoxRepository.GetByIdAsync(cashBoxDto.Id);
            if (old == null)
            {
                var cashBox = _mapper.Map<CashBoxRequestModel, CashBox>(cashBoxDto);
                if (cashBox == null)
                    return new ErrorResult(new Exception(), "Ошибка при обработке данных");
                await _cashBoxRepository.AddAsync(cashBox);
            }
            else
            {
                _mapper.Map(cashBoxDto, old);
            }

            await _cashBoxRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetCashBoxCashAsync(int cashBoxId)
    {
        try
        {
            var cashBox = await _cashBoxRepository.GetByIdAsync(cashBoxId);
            if (cashBox == null)
                return new ErrorResult(new Exception(), "Не удалось найти эту кассу");

            return new OkResult<CashBoxGetCashResponseModel>(
                _mapper.Map<CashBox, CashBoxGetCashResponseModel>(cashBox));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetAllAsync(int userId)
    {
        try
        {
            var res = await _cashBoxRepository.GetAllAsync(userId);
            return new OkResult<List<CashBoxResponseModel>>(res
                .Select(s => _mapper.Map<CashBox, CashBoxResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}