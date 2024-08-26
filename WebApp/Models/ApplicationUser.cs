using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool Active { get; set; }
        public bool IsFirstLogin { get; set; } 
    }
}
