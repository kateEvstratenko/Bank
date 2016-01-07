using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BLL.AutoMapper;
using BLL.Services;
using Newtonsoft.Json;

namespace BankServerApi
{
    public class WebApiApplication : HttpApplication
    {
        private DailyCalculateCreditService _dailyCalculateCreditService;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Configure();
            AutomapperConfig.Configure();
            AutoMapperConfiguration.Register();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            
            AuthManagerService.UserManager = Startup.UserManagerFactory();
            AuthManagerService.RoleManager = Startup.RoleManagerFactory();

            _dailyCalculateCreditService = new DailyCalculateCreditService();
        }
    }
}
