using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace DDM.EntityFrameworkCore
{
    public static class DDMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<DDMDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<DDMDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}