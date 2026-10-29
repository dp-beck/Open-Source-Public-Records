using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class OpenRecordsContext(DbContextOptions<OpenRecordsContext> options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<Resource> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .Property(c => c.CreatedOn)
            .HasDefaultValueSql("GETDATE()");    
        
        modelBuilder.Entity<State>()
            .Property(c => c.CreatedOn)
            .HasDefaultValueSql("GETDATE()"); 
        
        modelBuilder.Entity<County>()
            .Property(c => c.CreatedOn)
            .HasDefaultValueSql("GETDATE()"); 
        
        modelBuilder.Entity<Resource>()
            .Property(c => c.CreatedOn)
            .HasDefaultValueSql("GETDATE()"); 
    }
}