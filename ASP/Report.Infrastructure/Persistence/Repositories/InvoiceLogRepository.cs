﻿using Report.Application.Common.Interfaces.Repositories;
using Report.Domain.Models;
using Report.Infrastructure.Persistence.DataBase;

namespace Report.Infrastructure.Persistence.Repositories;

public class InvoiceLogRepository : Repository<InvoiceLog>, IInvoiceLogRepository
{
    public InvoiceLogRepository(DataContext context) : base(context)
    {
    }
}