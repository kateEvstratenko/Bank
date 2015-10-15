using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class AppUser : IdentityUser
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        //public string Email { get; set; }
        public string Password { get; set; }
//        public int WrongPasswordCount { get; set; }
//        public DateTime? EndBlockDateTime { get; set; }
        public virtual Customer Customer { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}