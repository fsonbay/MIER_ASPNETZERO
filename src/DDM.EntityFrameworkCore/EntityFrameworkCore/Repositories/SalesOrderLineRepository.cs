using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.SalesOrderLines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ISalesOrderLineRepository : IRepository<SalesOrderLine, int>
    {
        List<SalesOrderLine> GetSalesOrderLines (int salesOrderId);

    }

    public class SalesOrderLineRepository : DDMRepositoryBase<SalesOrderLine, int>, ISalesOrderLineRepository
    {
        private readonly IDbContextProvider<DDMDbContext> _dbContextProvider;
        private DDMDbContext _ctx => _dbContextProvider.GetDbContext();

        public SalesOrderLineRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public List<SalesOrderLine> GetSalesOrderLines(int salesOrderId)
        {
            return GetAll()
                .Where(s => s.SalesOrderId == salesOrderId)
                .ToList();
        }
    }

}
