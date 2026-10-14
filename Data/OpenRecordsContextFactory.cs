using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data;

public class OpenRecordsContextFactory: IDesignTimeDbContextFactory<OpenRecordsContext>
{
    public OpenRecordsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OpenRecordsContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=open-source-resources-server.database.windows.net;Initial Catalog=open-source-resources;Authentication=Active Directory Default;Encrypt=True;");
        return new OpenRecordsContext(optionsBuilder.Options);
    }
}