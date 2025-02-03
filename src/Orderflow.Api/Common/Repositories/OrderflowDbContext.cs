using Microsoft.EntityFrameworkCore;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Orders.Common.Repositories;
using Orderflow.Features.Trades.Common.Repositories;

namespace Orderflow.Common.Repositories;

public class OrderflowDbContext(DbContextOptions<OrderflowDbContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<ExchangeEntity> Exchanges { get; set; }
    public DbSet<InstrumentEntity> Instruments { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<TradeEntity> Trades { get; set; }
    public DbSet<OutboxEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxEvent>()
            .HasIndex(e => e.StreamId);

        modelBuilder.Entity<ExchangeEntity>()
            .HasIndex(e => e.Abbreviation)
            .IsUnique();

        modelBuilder.Entity<ExchangeEntity>()
            .HasIndex(e => e.Mic)
            .IsUnique();

        modelBuilder.Entity<ExchangeEntity>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<InstrumentEntity>()
            .HasOne(o => o.Exchange)
            .WithMany(e => e.Instruments)
            .HasForeignKey(i => i.ExchangeId);

        modelBuilder.Entity<InstrumentEntity>()
            .HasIndex(i => i.Ticker)
            .IsUnique();

        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.Instrument)
            .WithMany(i => i.Orders)
            .HasForeignKey(o => o.InstrumentId);

        modelBuilder.Entity<TradeEntity>()
            .HasOne(t => t.BuyOrder)
            .WithMany(o => o.BuyTrades)
            .HasForeignKey(o => o.BuyOrderId);
        modelBuilder.Entity<TradeEntity>()
            .HasOne(t => t.SellOrder)
            .WithMany(o => o.SellTrades)
            .HasForeignKey(o => o.SellOrderId);
    }
}