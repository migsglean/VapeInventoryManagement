using Microsoft.AspNetCore.Identity;

namespace VapeInventoryManagement.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Admin or User
    }
}
