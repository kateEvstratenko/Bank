using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    public class AppUserManagerFactory
    {
        private static AppUserManagerFactory _instance;
        private static readonly object SyncRoot = new object();

        private AppUserManagerFactory()
        {
            _userManager = () => new UserManager<AppUser>(new UserStore<AppUser>(new BankContext()))
            {
                PasswordValidator = new PasswordValidator()
                {
                    RequiredLength = 8,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                }
            };
        }
        private static Func<UserManager<AppUser>> _userManager;

        public Func<UserManager<AppUser>> Factory
        {
            get { return _userManager; }
        }

        public static AppUserManagerFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                        {
                            return _instance = new AppUserManagerFactory();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
