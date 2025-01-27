using BackEnd.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();

        base.OnConfiguring(optionsBuilder);
    }

    public virtual DbSet<Employee> Employee { get; set; }
    public virtual DbSet<PositionEmployee> PositionEmployee { get; set; }
    public virtual DbSet<PhoneEmployee> PhoneEmployee { get; set; }
}
