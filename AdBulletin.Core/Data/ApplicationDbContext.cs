using AdBulletin.Common.Constants;
using AddBulletin.Domain.Data.Configurations;
using AddBulletin.Domain.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdBulletin.Core.Data
{
    public class ApplicationDbContext : BaseContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var schema = Constants.System.PRODUCT_MAIN;

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new ApplicationRoleConfigurations());
            SetApplicationIdentityConfiguration(builder);
        }

        private ModelBuilder SetApplicationIdentityConfiguration(ModelBuilder builder)
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
    }
}
