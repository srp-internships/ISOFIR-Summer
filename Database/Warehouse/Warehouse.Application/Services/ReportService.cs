using AutoMapper;
using Warehouse.Application.Common.Interfaces.Repositories;
using Warehouse.Application.Common.Interfaces.Services;
using Warehouse.Application.ResponseModels;
using WareHouse.Core.Models;

namespace Warehouse.Application.Services;

public class ReportService:IReportService
{
    private readonly ISaleLogRepository _saleLogRepository;
    private readonly IMapper _mapper;
    private readonly IInvoiceLogRepository _invoiceLogRepository;

    public ReportService(IInvoiceLogRepository invoiceLogRepository, ISaleLogRepository saleLogRepository, IMapper mapper)
    {
        _invoiceLogRepository = invoiceLogRepository;
        _saleLogRepository = saleLogRepository;
        _mapper = mapper;
    }

    public List<ClientsHistoryResponseModel> GetClientsHistory()
    {
        return _saleLogRepository.GetIEnumerable().Select(s => _mapper.Map<SaleLog, ClientsHistoryResponseModel>(s).BuildIncome())
            .ToList();
    }

    public List<InvoiceHistoryResponseModel> GetInvoicesHistory()
    {
        return _invoiceLogRepository.GetIEnumerable()
            .Select(s => _mapper.Map<InvoiceLog, InvoiceHistoryResponseModel>(s)).ToList();
    }
}