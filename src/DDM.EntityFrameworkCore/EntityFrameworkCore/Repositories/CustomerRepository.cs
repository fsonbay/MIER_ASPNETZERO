using Abp.Data;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using DDM.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDM.EntityFrameworkCore.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }

    public class CustomerRepository : DDMRepositoryBase<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
              : base(dbContextProvider)
        {
        }

    }
}
