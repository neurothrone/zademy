using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public class ZademyDbContext(DbContextOptions<ZademyDbContext> options) : DbContext(options)
{
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<CourseEntity> Courses => Set<CourseEntity>();
    public DbSet<CourseInstanceEntity> CourseInstances => Set<CourseInstanceEntity>();
    public DbSet<GradeEntity> Grades => Set<GradeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure CourseInstanceEntity
        modelBuilder.Entity<CourseInstanceEntity>()
            .HasMany(ci => ci.Students)
            .WithMany();

        // Configure GradeEntity
        modelBuilder.Entity<GradeEntity>()
            .HasOne(g => g.Student)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GradeEntity>()
            .HasIndex(g => new { g.StudentId, g.CourseInstanceId })
            .IsUnique();
    }
}