using Microsoft.EntityFrameworkCore;
using Report.Core.Models;

namespace Report.Infrastructure.Persistence.DataBase;

public class DataContext : DbContext
{
    public DataContext()
    {
        Firms = Set<Firm>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;user=root;database=reportNew");
        // optionsBuilder.UseSqlite("Data Source=C:/ProgramData/Report/MainDb.db");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Firm> Firms { get; set; }
    public DbSet<CashBox> CashBoxes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InvoiceLog> InvoiceLogs { get; set; }
    public DbSet<SaleLog> SaleLogs { get; set; }
    public DbSet<RestProduct> RestProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("BINARY");
        base.OnModelCreating(modelBuilder);
    }
}