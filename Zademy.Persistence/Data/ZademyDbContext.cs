using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public class ZademyDbContext(DbContextOptions<ZademyDbContext> options) : DbContext(options)
{
    public DbSet<StudentEntity> Students => Set<StudentEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}