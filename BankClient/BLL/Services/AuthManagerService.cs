using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    public static class AuthManagerService
    {
        public static UserManager<AppUser> UserManager { get; set; }
        public static RoleManager<IdentityRole> RoleManager { get; set; }
    }
}
