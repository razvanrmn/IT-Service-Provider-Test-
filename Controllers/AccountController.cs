﻿using MultiLanguage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MultiLanguage.Controllers
{

    public class AccountController : Controller
    {
        
        private MainDbContext context = new MainDbContext();
        DateTimeOffset dateTimeNow = DateTimeOffset.Now;
        public ActionResult Index()
        {
            Singleton.Instance.writeMessage("Account index button clicked at: " + dateTimeNow.LocalDateTime);
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return View(context.Accounts.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            AccountModel model = new AccountModel();
            Singleton.Instance.writeMessage("Create button clicked at: " + dateTimeNow.LocalDateTime);
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(AccountModel model)
        {
            Singleton.Instance.writeMessage("Create button clicked at: " + dateTimeNow.LocalDateTime);
            var user = context.Accounts.SingleOrDefault(x => x.Username == model.Username || x.Password == model.Password || x.Email == model.Email);
            string pass = Crypto.Class.cryptPass(model.Password);
            if (user == null)
            {
                model.Password = pass;
                context.Accounts.Add(model);
                context.SaveChanges();
            }
            else
            {
               
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Modify(int id) {
            Singleton.Instance.writeMessage("Modify button clicked at: " + dateTimeNow.LocalDateTime);
            AccountModel accountModel = context.Accounts.Find(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return View(accountModel);
        }
        [HttpPost]
        public ActionResult Modify(AccountModel accountModel)
        {
            Singleton.Instance.writeMessage("Modify button clicked at: " + dateTimeNow.LocalDateTime);
            context.Entry(accountModel).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return RedirectToAction("Index");   
        }
        public ActionResult ViewDetails(int id)
        {
            Singleton.Instance.writeMessage("View account details button clicked at: " + dateTimeNow.LocalDateTime);
            AccountModel details = context.Accounts.Find(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return View(details);
        }
        public ActionResult Delete(int? id)
        {
            Singleton.Instance.writeMessage("Delete button clicked at: " + dateTimeNow.LocalDateTime);
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            AccountModel model = context.Accounts.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            Thread t = new Thread(new ThreadStart(Singleton.WriteToConsole));
            t.Start();
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Singleton.Instance.writeMessage("Delete button clicked at: " + dateTimeNow.LocalDateTime);
            if (ModelState.IsValid)
            {
                AccountModel accountModel = context.Accounts.Find(id);
                context.Accounts.Remove(accountModel);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}