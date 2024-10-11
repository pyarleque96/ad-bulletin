using AdBulletin.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdBulletin.Domain.Data.Context;

public class BaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
{
    public BaseContext()
    {
    }

    public BaseContext(DbContextOptions options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        AuditEntities();

        return base.SaveChanges();
    }

    private void AuditEntities()
    {
        var currentUtcDateTime = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            const StringComparison currentCultureIgnoreCase = StringComparison.CurrentCultureIgnoreCase;

            if (entry.State == EntityState.Added)
            {
                if (entry.Properties.Any(p => string.Equals(p.Metadata.Name, "CreatedAt", currentCultureIgnoreCase)))
                    entry.Property("CreatedAt").CurrentValue = currentUtcDateTime;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Properties.Any(p => string.Equals(p.Metadata.Name, "UpdatedAt", currentCultureIgnoreCase)))
                    entry.Property("UpdatedAt").CurrentValue = currentUtcDateTime;
            }
        }
    }
}
