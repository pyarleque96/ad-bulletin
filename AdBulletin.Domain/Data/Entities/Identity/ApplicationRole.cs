using Microsoft.AspNetCore.Identity;

namespace AddBulletin.Domain.Data.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
        }

        public int StatusId { get; set; }
    }
}
