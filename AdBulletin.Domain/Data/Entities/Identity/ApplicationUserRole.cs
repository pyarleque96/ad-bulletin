using Microsoft.AspNetCore.Identity;

namespace AdBulletin.Domain.Data.Entities
{
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        public ApplicationUserRole()
        {
        }
    }
}
