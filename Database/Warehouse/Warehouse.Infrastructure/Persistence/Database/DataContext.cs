using Microsoft.EntityFrameworkCore;
using WareHouse.Core.Models;

namespace Warehouse.Infrastructure.Persistence.Database;

public class DataContext:DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Rest> Rests { get; set; } = null!;
    public DbSet<InvoiceLog> InvoiceLogs { get; set; } = null!;
    public DbSet<SaleLog> SaleLogs { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;user=root;database=warehouse");   
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("BINARY");
        base.OnModelCreating(modelBuilder);
    }
}