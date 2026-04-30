using System.IO;
using System.Reflection;
using Interview_Test.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Interview_Test.Infrastructure;

public class InterviewTestDbContext : DbContext
{
    public InterviewTestDbContext(DbContextOptions<InterviewTestDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> UserTb { get; set; }
    public DbSet<UserProfileModel> UserProfileTb { get; set; }
    public DbSet<RoleModel> RoleTb { get; set; }
    public DbSet<UserRoleMappingModel> UserRoleMappingTb { get; set; }
    public DbSet<PermissionModel> PermissionTb { get; set; }
    public DbSet<RolePermissionMappingModel> RolePermissionMappingTb { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserRoleMappingModel>(entity =>
        {
            entity.HasOne(urm => urm.User)
                  .WithMany(u => u.UserRoleMappings)
                  .HasForeignKey("UserId")
                  .HasPrincipalKey(u => u.Id)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(urm => urm.Role)
                  .WithMany(r => r.UserRoleMappings)
                  .HasForeignKey("RoleId")
                  .HasPrincipalKey(r => r.Id)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<RolePermissionMappingModel>(entity =>
        {
            entity.HasOne(rpm => rpm.Role)
                  .WithMany(r => r.RolePermissionMappings)
                  .HasForeignKey("RoleId")
                  .HasPrincipalKey(r => r.Id)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(rpm => rpm.Permission)
                  .WithMany(p => p.RolePermissionMappings)
                  .HasForeignKey("PermissionId")
                  .HasPrincipalKey(p => p.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

public class InterviewTestDbContextDesignFactory : IDesignTimeDbContextFactory<InterviewTestDbContext>
{
    public InterviewTestDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("../Interview-Test.Api/appsettings.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<InterviewTestDbContext>()
            .UseSqlServer(connectionString, opts => opts.CommandTimeout(600));

        return new InterviewTestDbContext(optionsBuilder.Options);
    }
}