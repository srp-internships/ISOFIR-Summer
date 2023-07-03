using Warehouse.Application.Common.Interfaces.Repositories;
using WareHouse.Core.Models;
using Warehouse.Infrastructure.Persistence.Database;

namespace Warehouse.Infrastructure.Persistence.Repositories;

public class InvoiceLogRepository : IInvoiceLogRepository
{
    private readonly DataContext _context;

    public InvoiceLogRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(InvoiceLog invoiceLog)
    {
        _context.InvoiceLogs.Add(invoiceLog);
    }

    public IEnumerable<InvoiceLog> GetIEnumerable()
    {
        return _context.InvoiceLogs;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}


internal class SaleRepository:ISaleLogRepository
{
    private readonly DataContext _context;

    public SaleRepository(DataContext context)
    {
        _context = context;
    }

    public void Add(SaleLog saleLog)
    {
        _context.SaleLogs.Add(saleLog);
    }

    public IEnumerable<SaleLog> GetIEnumerable()
    {
        return _context.SaleLogs;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}