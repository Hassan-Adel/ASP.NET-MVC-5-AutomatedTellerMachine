using AutomatedTellerMachine.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        IDbSet<Transaction> Transactions { get; set; }
        int SaveChanges();
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /*
         *  One thing you won't get from AppHarbor is an offer to execute Code First migrations.
         *  Solution :
          this sets up a particular type of Database Initializer.
            Specifically, the Migrate Database to Latest Version type. 
            There are a few other types you can use that will automatically drop and recreate the database or only do so when the model changes,
            or only create the database if it doesn't already exist. 
            But Migrate To Latest Version is a good choice that you can use for your first deployment as well as subsequent deployments after your model changes. 
            The default implementation of this method does some setup work for ASP dot NET Identity models, 
            so we need to make sure we call Base dot OnModelCreating as well.
             */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }

        public IDbSet<Transaction> Transactions { get; set; }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<CheckingAccount> CheckingAccounts { get; set; }

        public IDbSet<Transaction> Transactions { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}