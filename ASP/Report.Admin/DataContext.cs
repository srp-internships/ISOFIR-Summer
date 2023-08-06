using System;
using Microsoft.EntityFrameworkCore;
using Report.Domain.Models;

namespace Report.Admin;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ClientCashLog> ClientCashLogs { get; set; }
    public DbSet<ReasonCashLog> ReasonCashLogs { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Reason> Reasons { get; set; }
    public DbSet<MoveProductLog> MoveProductLogs { get; set; }
    public DbSet<Firm> Firms { get; set; }
    public DbSet<CashBox> CashBoxes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<InvoiceLog> InvoiceLogs { get; set; }
    public DbSet<SaleLog> SaleLogs { get; set; }

    public DbSet<RestProduct> RestProducts { get; set; }
    // public DataContext()
    // {
    // Firms = Set<Firm>();
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.UseMySQL(
            "Persist Security Info=False;database=reportNew;server=localhost;port=3306;user id=root;Password=;");
        // optionsBuilder.UseSqlite("Data Source=C:/ProgramData/Report/MainDb.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.UseCollation("BINARY");

        modelBuilder.Entity<MoveProductLog>().HasIndex(p => p.FromStorageId).IsUnique(false);
        modelBuilder.Entity<MoveProductLog>().HasIndex(p => p.ToStorageId).IsUnique(false);
        modelBuilder.Entity<MoveProductLog>().HasOne(p => p.FromStorage).WithMany(f => f.MoveProductsLogsFrom);
        modelBuilder.Entity<MoveProductLog>().HasOne(p => p.ToStorage).WithMany(f => f.MoveProductsLogsTo);
        base.OnModelCreating(modelBuilder);
    }
}