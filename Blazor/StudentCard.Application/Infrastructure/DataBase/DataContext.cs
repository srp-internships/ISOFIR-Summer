using Microsoft.EntityFrameworkCore;
using StudentCard.Domain.Models;

namespace StudentCard.Application.Infrastructure.DataBase;

public sealed class DataContext:DbContext
{
    internal static string ConnectionString="server=localhost;user=root;database=StudentCardDb";

    public DataContext()
    {
        Students = Set<Student>();
        Pays = Set<Pay>();
        Agents = Set<Agent>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Agent> Agents { get; }
    public DbSet<Pay> Pays { get; }
    public DbSet<Student> Students { get; }
}