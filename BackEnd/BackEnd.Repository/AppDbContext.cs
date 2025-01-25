using BackEnd.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<PositionEmployee> PositionEmployee { get; set; }
    public DbSet<PhoneEmployee> PhoneEmployee { get; set; }
}
