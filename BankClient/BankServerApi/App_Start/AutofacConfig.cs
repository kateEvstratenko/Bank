using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using BankServerApi.CustomAttributes;

namespace BankServerApi
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<EmailSender>().As<IEmailSender>();
            builder.RegisterType<CreditService>().As<ICreditService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<EncryptorService>().As<IEncryptorService>();
            builder.RegisterType<CalculationCreditService>().As<ICalculationCreditService>();

            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();            
            builder.RegisterType<CreditRepository>().As<ICreditRepository>();
            builder.RegisterType<TokenRepository>().As<ITokenRepository>();
            builder.RegisterType<CreditRequestRepository>().As<ICreditRequestRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<BankContext>().AsSelf();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}