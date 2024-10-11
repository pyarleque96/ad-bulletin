using AdBulletin.Common.Constants;
using AdBulletin.Domain.Data.Configurations;
using AdBulletin.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdBulletin.Domain.Data.Context;

public class ApplicationDbContext : BaseContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdCategory> AdCategories { get; set; }
    public virtual DbSet<Ad> Ads { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var schema = Constants.System.PRODUCT_MAIN;

        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());
        builder.SetApplicationIdentityConfiguration();

        builder.ApplyConfiguration(new AdCategoryConfiguration());
        builder.ApplyConfiguration(new AdConfiguration());

        builder.Seed();
    }
}

public static class ModelBuilderExtensions
{
    public static ModelBuilder SetApplicationIdentityConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ApplicationUserRole>(entity =>
        {
            entity.ToTable("AspNetUserRoles");
        });

        builder.Entity<ApplicationUserClaim>(entity =>
        {
            entity.ToTable("AspNetUserClaims");
        });

        builder.Entity<ApplicationUserLogin>(entity =>
        {
            entity.ToTable("AspNetUserLogins");
        });

        builder.Entity<ApplicationRoleClaim>(entity =>
        {
            entity.ToTable("AspNetRoleClaims");
        });

        builder.Entity<ApplicationUserToken>(entity =>
        {
            entity.ToTable("AspNetUserTokens");
        });

        return builder;
    }

    public static ModelBuilder Seed(this ModelBuilder builder)
    {
        SeedRoles(builder);

        return builder;
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        var roles = Constants.Roles.LIST_ROLES
            .Select((role, index) => new ApplicationRole
            {
                Id = index + 1, // Start from 1
                Name = role,
                NormalizedName = role.ToUpper(),
                StatusId = (int)Constants.Status.BasicStatus.Active,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            })
            .ToList();

        builder.Entity<ApplicationRole>().HasData(roles);
    }
}