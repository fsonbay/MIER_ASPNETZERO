using Abp.Data;
using Abp.EntityFrameworkCore;
using DDM.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDM.EntityFrameworkCore.Repositories
{
    public class CustomerRepository : DDMRepositoryBase<Customer, int>
    {
        protected CustomerRepository(IDbContextProvider<DDMDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


 



    }
}
