using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class KambaiaAPIDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public KambaiaAPIDbContext(DbContextOptions<KambaiaAPIDbContext> options) : base(options) { }
}