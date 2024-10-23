using Microsoft.EntityFrameworkCore;
using OrderFlow.Data.Entities;

namespace OrderFlow.Data.DbContext;

public class OrderflowDbContext(DbContextOptions<OrderflowDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Instrument> Instruments { get; init; }
    public DbSet<Order> Orders { get; init; }
}