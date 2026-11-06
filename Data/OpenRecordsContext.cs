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
        
        modelBuilder.Entity<Resource>()
            .HasOne(r => r.Country)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Resource>()
            .HasOne(r => r.State)
            .WithMany(s => s.Resources)
            .HasForeignKey(r => r.StateId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Resource>()
            .HasOne(r => r.County)
            .WithMany(c => c.Resources)
            .HasForeignKey(r => r.CountyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}