using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.SalesOrderLines;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ISalesOrderLineRepository : IRepository<SalesOrderLine, int>
    {

    }

    public class SalesOrderLineRepository : DDMRepositoryBase<SalesOrderLine, int>, ISalesOrderLineRepository
    {
        public SalesOrderLineRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

    }

}
