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

        // GET: Transaction/Transfer
        public ActionResult Transfer(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(TransferViewModel transfer)
        {
            var userCheckingAccount = db.CheckingAccounts.Find(transfer.CheckingAccountId);
            
            // check for available funds
            if (userCheckingAccount.Balance < transfer.Amount)
            {
                ModelState.AddModelError("Amount", "You have insufficient funds!");
            }

            // I'm using first, or default here, so that it will return null if the checking account number isn't valid, instead of throwing an exception.
            var destinationCheckingAccount = db.CheckingAccounts.Where(c => c.AccountNumber == transfer.DestinationCheckingAccountNumber).FirstOrDefault();
            
            // check for a valid destination account
            if (destinationCheckingAccount == null)
            {
                ModelState.AddModelError("DestinationCheckingAccountNumber", "Invalid Destination Checking Account Number !");
            }

            if (ModelState.IsValid)
            {
                db.Transactions.Add(new Transaction { CheckingAccountId = userCheckingAccount.Id , Amount = -transfer.Amount });
                db.Transactions.Add(new Transaction { CheckingAccountId = destinationCheckingAccount.Id, Amount = transfer.Amount });
                db.SaveChanges();

                var service = new CheckingAccountService(db);
                service.UpdateBalance(userCheckingAccount.Id);
                service.UpdateBalance(destinationCheckingAccount.Id);

                return PartialView("_TransferSuccess", transfer);
            }
            return PartialView("_TransferForm");

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

        public ActionResult QuickCash(int checkingAccountId, decimal amount)
        {
            //Negative amount because we're withdrawing cash
            var transaction = new Transaction { CheckingAccountId = checkingAccountId, Amount = -amount };
            var userCheckingAccount = db.CheckingAccounts.Find(transaction.CheckingAccountId);
            if (userCheckingAccount.Balance < transaction.Amount)
            {
                return View("QuickCashInsufficientFunds");
            }
            db.Transactions.Add(transaction);
            db.SaveChanges();
            var service = new CheckingAccountService(db);
            service.UpdateBalance(transaction.CheckingAccountId);
            return RedirectToAction("Index", "Home");
        }


    }
}