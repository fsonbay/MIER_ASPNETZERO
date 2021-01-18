using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.SalesOrders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ISalesOrderRepository : IRepository<SalesOrder, int>
    {
        int CountCustomerOrder(int customerId);


    }
    public class SalesOrderRepository : DDMRepositoryBase<SalesOrder, int>, ISalesOrderRepository
    {
        //private readonly IDbContextProvider<DDMDbContext> _dbContextProvider;
        //private DDMDbContext _ctx => _dbContextProvider.GetDbContext();

        public SalesOrderRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
              : base(dbContextProvider)
        {
            //_dbContextProvider = dbContextProvider;
        }

        public int CountCustomerOrder(int customerId)
        {
            var count = GetAll().Where(c => c.CustomerId == customerId).Count();
            return count;

        }

        public SalesOrder GetIncludes(int salesOrderId)
        {
            return GetAll()
                .Where(s => s.Id == salesOrderId)
                .Include(s => s.SalesOrderLines)
               // .Include(p => p.ProductionStatus)
                .FirstOrDefault();
        }

        //public SalesOrder test(int id)
        //{
        //    return _ctx.SalesOrders
        //        .Where(p => p.Id == id)
        //        .FirstOrDefault();
        //}

    }
}
