using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Context;

public class EfCoreContext : DbContext
{

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

