using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.SalesInvoices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ISalesInvoiceRepository : IRepository<SalesInvoice, int>
    {
        //Methods
    }


    public class SalesInvoiceRepository : DDMRepositoryBase<SalesInvoice, int>, ISalesInvoiceRepository
    {
        public SalesInvoiceRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
    }
}
