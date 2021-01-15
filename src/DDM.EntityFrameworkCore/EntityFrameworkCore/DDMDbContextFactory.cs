using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DDM.Configuration;
using DDM.Web;

namespace DDM.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DDMDbContextFactory : IDesignTimeDbContextFactory<DDMDbContext>
    {
        public DDMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DDMDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            DDMDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DDMConsts.ConnectionStringName));

            return new DDMDbContext(builder.Options);
        }
    }
}