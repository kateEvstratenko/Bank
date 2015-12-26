using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;

namespace ClientApi
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
            builder.RegisterType<DepositService>().As<IDepositService>();
            builder.RegisterType<CreditRequestService>().As<ICreditRequestService>();
            builder.RegisterType<CalculationDebtService>().As<ICalculationDebtService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<EncryptorService>().As<IEncryptorService>();
            builder.RegisterType<CalculationCreditService>().As<ICalculationCreditService>();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<CustomerCreditService>().As<ICustomerCreditService>();
            builder.RegisterType<CustomerDepositService>().As<ICustomerDepositService>();
            builder.RegisterType<BillService>().As<IBillService>();
            //            builder.RegisterType<DailyCalculateService>().AsSelf().SingleInstance();

            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<CreditRepository>().As<ICreditRepository>();
            builder.RegisterType<DepositRepository>().As<IDepositRepository>();
            builder.RegisterType<TokenRepository>().As<ITokenRepository>();
            builder.RegisterType<CreditRequestRepository>().As<ICreditRequestRepository>();
            builder.RegisterType<CreditPaymentPlanItemRepository>().As<ICreditPaymentPlanItemRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<CustomerCreditRepository>().As<ICustomerCreditRepository>();
            builder.RegisterType<CustomerDepositRepository>().As<ICustomerDepositRepository>();
            builder.RegisterType<BillRepository>().As<IBillRepository>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<BankContext>().AsSelf(); ;

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}