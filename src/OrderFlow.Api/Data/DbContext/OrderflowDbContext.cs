using Microsoft.EntityFrameworkCore;
using OrderFlow.Data.Entities;

namespace OrderFlow.Data.DbContext;

public class OrderflowDbContext(DbContextOptions<OrderflowDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<InstrumentEntity> Instruments { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.Instrument)
            .WithMany(i => i.Orders)
            .HasForeignKey(o => o.InstrumentId);
    }
}