using System.Data.Entity.Migrations;
using System.Linq;
using Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BankContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var roles = EnumHelper.GetValues<AppRoles>().ToList();
            var identityRoles = new IdentityRole[roles.Count];
            for (var i = 0; i < roles.Count; i++)
            {
                identityRoles[i] = new IdentityRole(roles[i].ToString());
            }
            context.Roles.AddOrUpdate(r => r.Name, identityRoles);
        }
    }
}