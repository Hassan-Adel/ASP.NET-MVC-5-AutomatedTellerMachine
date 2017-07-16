﻿using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    // [Authorize] // Allows any logged in user iapplied to the whole class
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET /home/index
        // [Authorize] // Allows any logged in user
        [MyLoggingFilter]
        public ActionResult Index()
        {
            // throw new StackOverflowException();
            var userId = User.Identity.GetUserId();
            try { 
            var checkingAccountId = db.CheckingAccounts.Where(checkingAccount => checkingAccount.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET /home/about
        //[ActionName("about-this-atm")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }
                
        [AllowAnonymous] // Allows any user
        public ActionResult Contact()
        {
            /*
             The view bag is a dynamic object that we use to pass information between the controller and the view 
             that doesn't belong in a model since it's not real application data. It's a little bit like a session object, 
             except it's going to be disposed of as soon as your view is rendered, and it's only available if you return a
             view at the end of your method.
             */
            ViewBag.TheMessage = "Having trouble? Send us a message.";
            /*
             If you need something that would survive a redirect, you can use the temp data dictionary and assign a value for the message key like so
             TempData["Message"] = test
             */

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            // TODO: send the message to HQ
            
            ViewBag.TheMessage = "Thanks, we got your message!";

            return View();
        }

        public ActionResult Foo()
        {            
            return View("About");
        }

        public ActionResult Serial(string letterCase)
        {
            var serial = "ASPNETMVC5ATM1";
            if (letterCase == "lower")
            {
                return Content(serial.ToLower());
            }
            // return new HttpStatusCodeResult(403);
            //return Json(new { name = "serial number", value = "serial" }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
    }
}