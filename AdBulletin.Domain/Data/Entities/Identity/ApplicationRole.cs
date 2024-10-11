using Microsoft.AspNetCore.Identity;

namespace AdBulletin.Domain.Data.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ApplicationRole()
        {
        }

        public int StatusId { get; set; }
    }
}
