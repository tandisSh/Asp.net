using Microsoft.AspNetCore.Identity;
namespace ElinorStoreServer.Data.Entities
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
