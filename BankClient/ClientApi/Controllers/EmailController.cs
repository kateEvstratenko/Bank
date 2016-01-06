using System.Threading.Tasks;
using System.Web.Mvc;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ClientApi.Controllers
{
    public class EmailController : Controller
    {
        public EmailController()
            : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat)
        {
        }

        public EmailController(UserManager<AppUser> userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public UserManager<AppUser> UserManager { get; private set; }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            var user = UserManager.FindById(token);
            if (user == null)
            {
               return View(false);
            }
            if (user.Email != email)
            {
                return View(false);
            }
            user.EmailConfirmed = true;
            await UserManager.UpdateAsync(user);
            return View(true);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("ConfirmChangeEmail")]
        public async Task<ActionResult> ConfirmChangeEmail(string token, string email)
        {
            var user = UserManager.FindById(token);
            if (user == null)
            {
                return View(false);
            }
            user.Email = email;
            await UserManager.UpdateAsync(user);
            return View(true);
        }
    }
}
