using AutoMapper;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ReportService:IReportService
{
    private readonly IClientRepository _clientRepository;
    private readonly IInvoiceLogRepository _invoiceLogRepository;
    private readonly ISaleLogRepository _saleLogRepository;
    private readonly IMapper _mapper;
    public ReportService(IClientRepository clientRepository, IInvoiceLogRepository invoiceLogRepository, ISaleLogRepository saleLogRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _invoiceLogRepository = invoiceLogRepository;
        _saleLogRepository = saleLogRepository;
        _mapper = mapper;
    }

    public async Task<Result> GetInvoicesLogAsync()
    {
        try
        {
            var invoicesLogs = await _invoiceLogRepository.GetAllAsync();
            var result = new OkResult<List<InvoicesLogResponseModel>>(invoicesLogs
                .Select(s => _mapper.Map<InvoiceLog, InvoicesLogResponseModel>(s)).ToList());
            return result;
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public Task<Result> GetClientHistoryAsync(int clientId)
    { 
        throw new NotImplementedException();
    }
}