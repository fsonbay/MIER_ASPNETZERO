using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.SalesOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ISalesOrderRepository : IRepository<SalesOrder, int>
    {
        int CountCustomerOrder(int customerId);
    }
    public class SalesOrderRepository : DDMRepositoryBase<SalesOrder, int>, ISalesOrderRepository
    {
        public SalesOrderRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
              : base(dbContextProvider)
        {
        }

        public int CountCustomerOrder(int customerId)
        {
            var count = GetAll().Where(c => c.CustomerId == customerId).Count();
            return count;

        }
    }
}
