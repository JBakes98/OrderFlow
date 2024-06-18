using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Contexts;

// TODO: Change this to standat DbContext if AWS Cognito implemented
public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Instrument> Instruments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Event> Events { get; set; }
}