﻿using BackEnd.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employee { get; set; }
    public DbSet<PositionEmployee> PositionEmployee { get; set; }
}