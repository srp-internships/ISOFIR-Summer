using AutoMapper;
using OfficeOpenXml;
using Report.Application.Common.Interfaces.Repositories;
using Report.Application.Common.Interfaces.Services;
using Report.Application.ResponseModels;
using Report.Domain.ActionResults;
using Report.Domain.Models;

namespace Report.Application.Services;

public class ReportService : IReportService
{
    private readonly IClientRepository _clientRepository;
    private readonly IInvoiceLogRepository _invoiceLogRepository;
    private readonly IMapper _mapper;
    private readonly ISaleLogRepository _saleLogRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IFirmRepository _firmRepository;
    private readonly IRateService _rateService;

    public ReportService(IClientRepository clientRepository, IInvoiceLogRepository invoiceLogRepository,
        ISaleLogRepository saleLogRepository, IMapper mapper, IStorageRepository storageRepository, IFirmRepository firmRepository, IRateService rateService)
    {
        _clientRepository = clientRepository;
        _invoiceLogRepository = invoiceLogRepository;
        _saleLogRepository = saleLogRepository;
        _mapper = mapper;
        _storageRepository = storageRepository;
        _firmRepository = firmRepository;
        _rateService = rateService;
    }

    public async Task<Result> GetInvoicesLogAsync(int userId)
    {
        try
        {
            var invoicesLogs = await _invoiceLogRepository.GetAllAsync(userId);
            var result = new OkResult<List<InvoicesLogResponseModel>>(invoicesLogs
                .Select(s => _mapper.Map<InvoiceLog, InvoicesLogResponseModel>(s)).ToList());
            return result;
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetClientHistoryAsync(int clientId, int userId)
    {
        try
        {
            var clientHistory = await _saleLogRepository.GetClientHistoryAsync(clientId, userId);
            return new OkResult<List<SaleLogResponseModel>>(clientHistory
                .Select(s => _mapper.Map<SaleLogResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetStorageRestAsync(int storageId)
    {
        try
        {
            var storageRests = await _storageRepository.GetStorageRestsAsync(storageId);
            return new OkResult<List<RestProductResponseModel>>(storageRests
                .Select(s => _mapper.Map<RestProductResponseModel>(s)).ToList());
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetToDayTurnOverAsync(int userId)
    {
        try
        {
            var sales = await _saleLogRepository.GetToDaySalesAsync(userId);
            var result = sales.Sum(saleLog => saleLog.PriceUsd);
            
            return new OkResult<decimal>(result);
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetToDayClearAsync(int userId)
    {
        try
        {
            var sales = await _saleLogRepository.GetToDaySalesAsync(userId);

            var result = sales.Sum(saleLog => saleLog.PriceUsd * saleLog.Quantity - saleLog.RestProduct.InvoicePriceUsd * saleLog.Quantity);


            return new OkResult<decimal>(result);
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> GetToDaySalesQuantityAsync(int userId)
    {
        try
        {
            var sales = await _saleLogRepository.GetToDaySalesAsync(userId);
            return new OkResult<decimal>(sales.Sum(s=>s.Quantity));
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> ExportRestsToExcelAsync(int userId,string path)
    {
        try
        {
            var storages = await _storageRepository.GetStoragesWithRestsAsync(userId);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var pack = new ExcelPackage();
            var cells = pack.Workbook.Worksheets.Add("Rests").Cells;
            var i = 1;
            foreach (var storage in storages)
            {
                if (storage.RestProducts == null) continue;
                var rests = storage.RestProducts.Where(s => s.Quantity != 0);
                cells["A"+i].Value=storage.Name;
                i++;
                cells["A" + i].Value = "Имя продукта";
                cells["B"+i].Value="Себестоимость";
                cells["C"+i].Value="Количество";
                cells["D"+i].Value="Сумма";
                
                i++;
                foreach (var rest in rests)
                {
                    cells["A"+i].Value=rest.Product+"";
                    cells["B"+i].Value=rest.InvoicePriceUsd;
                    cells["C"+i].Value=rest.Quantity;
                    cells["D"+i].Value=rest.InvoicePriceUsd*rest.Quantity;
                    i++;
                }

                i++;
            }

            await pack.SaveAsAsync(path);
            
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }

    public async Task<Result> ExportDebtsToExcelAsync(int userId, string path)
    {
        try
        {
            var oneDollarIs = ((OkResult<decimal>)await _rateService.GetLastAsync(userId)).Result;
            
            var clients  = await _clientRepository.GetAllAsync(userId);
            clients = clients.OrderBy(s => s.CashTjs).ToList();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pack = new ExcelPackage();
            var cells = pack.Workbook.Worksheets.Add("Debts").Cells;
            var i = 1;
            
            cells["A"+i].Value="Клиент";
            cells["B"+i].Value="Счёт";
            
            
            foreach (var client in clients)
            {
                i++;
                cells["A"+i].Value=client.Name;
                cells["B"+i].Value=client.CashTjs / oneDollarIs+client.CashUsd;
            }

            i = 1;
            
            cells["A"+i].Value="Фирма";
            cells["B"+i].Value="Счёт";
            
            
            
            var firms = await _firmRepository.GetAllAsync(userId);
            firms = firms.OrderByDescending(s => s.CashUsd).ToList();
            
            foreach (var firm in firms)
            {
                i++;
                cells["A"+i].Value=firm.Name;
                cells["B"+i].Value=firm.CashUsd;
            }

            await pack.SaveAsAsync(path);
            return new OkResult();
        }
        catch (Exception e)
        {
            return new ErrorResult(e);
        }
    }
}