using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models2;

public class LocalDbContext : DbContext
{
    public DbSet<Student2> Student { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;User ID=SA;Password=<YourStrong@Passw0rd>; TrustServerCertificate=true;");
        base.OnConfiguring(optionsBuilder);
    }
}