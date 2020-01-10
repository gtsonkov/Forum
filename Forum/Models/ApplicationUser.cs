using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    using Microsoft.AspNetCore.Identity;
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
