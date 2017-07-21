namespace AutomatedTellerMachine.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Services;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AutomatedTellerMachine.Models.ApplicationDbContext";
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //context.Users.AddOrUpdate//in case there's already that user in the DB
            if(!context.Users.Any(u => u.UserName == "admin@mvcbank.com"))
            {
                var adminUser = new ApplicationUser { UserName = "admin@mvcbank.com", Email = "admin@mvcbank.com" };
                userManager.Create(adminUser, "Admin_2017");

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin", "user", adminUser.Id, 0);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();

                userManager.AddToRole(adminUser.Id, "Admin");
            }

            //context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = -25, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 100000, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 19.99m, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 64.40m, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 100, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = -300, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 255.75m, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 198, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 2, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 50, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = -1.50m, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 6100, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = 164.84m, CheckingAccountId = 9 });
            //context.Transactions.Add(new Transaction { Amount = .01m, CheckingAccountId = 9 });

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
        }
    }
}
