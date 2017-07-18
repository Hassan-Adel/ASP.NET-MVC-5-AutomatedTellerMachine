using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private IApplicationDbContext db;

        public TransactionController() {
            db = new ApplicationDbContext();
        }

        //Mockup for testing
        public TransactionController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        // GET: Transaction/Deposit
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        // GET: Transaction/Withdraw
        public ActionResult Withdraw(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(Transaction transaction)
        {
            var userCheckingAccount = db.CheckingAccounts.Find(transaction.CheckingAccountId);
            if(userCheckingAccount.Balance < transaction.Amount)
            {
                ModelState.AddModelError("Amount", "You have insufficient funds!");
            }
            
            if (ModelState.IsValid)
            {
                transaction.Amount = -transaction.Amount;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Deposit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBalance(transaction.CheckingAccountId);
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
    }
}