using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data;

public class OpenRecordsContextFactory: IDesignTimeDbContextFactory<OpenRecordsContext>
{
    public OpenRecordsContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        var optionsBuilder = new DbContextOptionsBuilder<OpenRecordsContext>();
        optionsBuilder.UseAzureSql(connectionString);
        return new OpenRecordsContext(optionsBuilder.Options);
    }
}
