using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        int IBaseEntity.Id { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }

        public virtual Customer Customer { get; set; }

//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
//            // Add custom user claims here
//            return userIdentity;
//        }
    }
}