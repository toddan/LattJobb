namespace Gottknark.Migrations
{
    using Gottknark.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Gottknark.DAL.GottKnarkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Gottknark.DAL.GottKnarkContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedMemberShip();
        }

        /// <summary>
        /// Seeds the simplemembership provider with ef code first migrations
        /// </summary>
        private void SeedMemberShip()
        {
            WebSecurity.InitializeDatabaseConnection(
                "smarterasp",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);
            if (!Roles.GetAllRoles().Contains("Employee"))
                Roles.CreateRole("Employee");
            if (!Roles.GetAllRoles().Contains("Employer"))
                Roles.CreateRole("Employer");

            if (!WebSecurity.UserExists("toddan"))
                WebSecurity.CreateUserAndAccount(
                    "toddan",
                    "toddan");

            if (!Roles.GetRolesForUser("toddan").Contains("Employer"))
                Roles.AddUsersToRoles(new[] { "toddan" }, new[] { "Employer" });


        }
    }
}
