using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.DbContext;

public class OrderflowDbContext(DbContextOptions<OrderflowDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<Order> Orders { get; set; }
}