using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ReasonCashBoxService : IReasonCashBoxService
{
    private readonly ICashBoxRepository _cashBoxRepository;
    private readonly IMapper _mapper;
    private readonly IReasonCashLogRepository _reasonCashLogRepository;

    public ReasonCashBoxService(IMapper mapper, IReasonCashLogRepository reasonCashLogRepository,
        ICashBoxRepository cashBoxRepository)
    {
        _mapper = mapper;
        _reasonCashLogRepository = reasonCashLogRepository;
        _cashBoxRepository = cashBoxRepository;
    }

    public async Task<Result> PayWithDraw(ReasonCashBoxRequestModel reasonCash)
    {
        try
        {
            var log = _mapper.Map<ReasonCashBoxRequestModel, ReasonCashLog>(reasonCash);
            if (log == null)
                return new ErrorResult(new Exception(), "Ошибка при обработке ваших данных");
            await _reasonCashLogRepository.AddAsync(log);

            var cashBox = await _cashBoxRepository.GetByIdAsync(reasonCash.CashBoxId);
            if (cashBox == null)
                return new ErrorResult(new Exception(), "Вы выбрали недействительную кассу");
            cashBox.CashTjs += reasonCash.CashTjs;
            cashBox.CashUsd += reasonCash.CashUsd;

            await _reasonCashLogRepository.SaveChangesAsync();
            await _cashBoxRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetAllHistoryAsync(int userId)
    {
        try
        {
            var all = await _reasonCashLogRepository.GetAllAsync(userId);
            return new OkResult<List<ReasonCashBoxResponseModel>>(all
                .Select(s => _mapper.Map<ReasonCashLog, ReasonCashBoxResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}