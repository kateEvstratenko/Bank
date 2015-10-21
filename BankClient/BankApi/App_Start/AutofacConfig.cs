﻿using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace BankApi
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
            builder.RegisterType<RegistrationService>().As<IRegistrationService>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>();
            builder.RegisterType<BankContext>().AsSelf();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}