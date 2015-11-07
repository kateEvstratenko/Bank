using System;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    public class AppRoleManagerFactory
    {
        private static AppRoleManagerFactory _instance;
        private static readonly object SyncRoot = new object();

        private static Func<RoleManager<IdentityRole>> _roleManager;

        private AppRoleManagerFactory()
        {
            _roleManager = () => new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new BankContext()));
        }

        public Func<RoleManager<IdentityRole>> Factory
        {
            get { return _roleManager; }
        }

        public static AppRoleManagerFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            return _instance = new AppRoleManagerFactory();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}