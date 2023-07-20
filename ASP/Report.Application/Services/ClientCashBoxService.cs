using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.RequestModels;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ClientCashBoxService:IClientCashBoxService
{
    private readonly IClientCashLogRepository _clientCashLogRepository;
    private readonly ICashBoxRepository _cashBoxRepository;
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public ClientCashBoxService(IMapper mapper, IClientCashLogRepository clientCashLogRepository, ICashBoxRepository cashBoxRepository, IClientRepository clientRepository)
    {
        _mapper = mapper;
        _clientCashLogRepository = clientCashLogRepository;
        _cashBoxRepository = cashBoxRepository;
        _clientRepository = clientRepository;
    }

    public async Task<Result> PayWithDraw(ClientCashBoxRequestModel clientCash)
    {
        try
        {
            var log = _mapper.Map<ClientCashBoxRequestModel, ClientCashLog>(clientCash);
            if (log == null)
                return new ErrorResult(new Exception(), "Ошибка при обработке ваших данных");
            await _clientCashLogRepository.AddAsync(log);

            var cashBox = await _cashBoxRepository.GetByIdAsync(clientCash.CashBoxId);
            if (cashBox == null)
                return new ErrorResult(new Exception(), "Вы выбрали недействительную кассу");
            cashBox.CashTjs += clientCash.CashTjs;
            cashBox.CashUsd += clientCash.CashUsd;


            var client = await _clientRepository.GetByIdAsync(clientCash.ClientId);
            if (client == null) return new ErrorResult(new Exception(), "Недопустимый клиент");
            client.CashTjs += clientCash.CashTjs;
            client.CashUsd += clientCash.CashUsd;

            await _clientRepository.SaveChangesAsync();
            await _clientCashLogRepository.SaveChangesAsync();
            await _cashBoxRepository.SaveChangesAsync();
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetAllHistoryAsync()
    {
        try
        {
            var all = await _clientCashLogRepository.GetAllAsync();
            return new OkResult<List<ClientCashBoxResponseModel>>(all
                .Select(s => _mapper.Map<ClientCashLog, ClientCashBoxResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}