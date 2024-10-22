using Microsoft.EntityFrameworkCore;

namespace OrderFlow.Models;

public class OrderflowDbContext(DbContextOptions<OrderflowDbContext> options) : DbContext(options)
{
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<Order> Orders { get; set; }
}