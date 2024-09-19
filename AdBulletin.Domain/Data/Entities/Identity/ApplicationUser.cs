using Microsoft.AspNetCore.Identity;

namespace AddBulletin.Domain.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<int> { }

    public class ApplicationRoleClaim : IdentityRoleClaim<int> { }

    public class ApplicationUserToken : IdentityUserToken<int> { }

    public class ApplicationUserClaim : IdentityUserClaim<int> { }
}
