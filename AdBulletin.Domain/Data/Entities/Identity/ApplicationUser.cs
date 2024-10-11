using Microsoft.AspNetCore.Identity;

namespace AdBulletin.Domain.Data.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string? GivenName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<long> { }

    public class ApplicationRoleClaim : IdentityRoleClaim<long> { }

    public class ApplicationUserToken : IdentityUserToken<long> { }

    public class ApplicationUserClaim : IdentityUserClaim<long> { }
}
