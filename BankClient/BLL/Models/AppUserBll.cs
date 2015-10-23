using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Models
{
    public class AppUserBll : IdentityUser
    {
        public int CustomerId { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
