using WareHouse.Core.Models;

namespace Warehouse.Application.Common.Interfaces.Repositories;

public interface IInvoiceLogRepository
{
    void Add(InvoiceLog invoiceLog);
    IEnumerable<InvoiceLog> GetIEnumerable();
    
    int Save();
}

public interface ISaleLogRepository
{
    void Add(SaleLog saleLog);
    IEnumerable<SaleLog> GetIEnumerable();
    
    int Save();
}