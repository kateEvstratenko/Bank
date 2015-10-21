using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Models
{
    public class AppUserBll : IdentityUser
    {
//        [ForeignKey("Customer")]
//        public int CustomerId { get; set; }
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }

//        public virtual Customer Customer { get; set; }
    }
}
