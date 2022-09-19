
using Microsoft.EntityFrameworkCore;

public class MvcDbContext : DbContext
{
    public MvcDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
}
