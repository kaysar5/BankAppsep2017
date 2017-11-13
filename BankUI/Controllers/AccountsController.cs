﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankApp;

namespace BankUI.Controllers
{
    public class AccountsController : Controller
    {
       

        // GET: Accounts
        [Authorize]
        public ActionResult Index()
        {
            return View(Bank.GetAllaccounts(HttpContext.User.Identity.Name));
        }

        // GET: Accounts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = Bank.GetAccountsByAccountNumber(id.Value);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "AccountNumber,EmailAddress,Balance,AccountType,CreatedDate")] Account account)
        {            
                account.EmailAddress = HttpContext.User.Identity.Name;
                Bank.CreateAccount(account.EmailAddress, account.AccountType);
                return RedirectToAction("Index");            
        }

        // GET: Accounts/Edit/5
        [Authorize
            ]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            { 
            Account account = Bank.GetAccountsByAccountNumber(id.Value);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
            }
            catch(InvalidAccountException ax)
            {
                Session["ErrorMessage"] = ax.Message;
                throw;
            }
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "AccountNumber,EmailAddress,Balance,AccountType,CreateDate")] Account account)
        {           
                Bank.EditAccount(account);
                return RedirectToAction("Index");            
        }
        [Authorize]
        
        public ActionResult Deposit(int?id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var account = Bank.GetAccountsByAccountNumber(id.Value);
            return View(account);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Deposit(FormCollection Controls)
        {
            var accountNumber = Convert.ToInt32(Controls["AccountNumber"]);
            var amount = Convert.ToDecimal(Controls["Amount"]);
            Bank.Deposit(accountNumber, amount);
            return RedirectToAction("Index");
        }
        [Authorize]

        public ActionResult Withdraw(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var account = Bank.GetAccountsByAccountNumber(id.Value);
            return View(account);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Withdraw(FormCollection Controls)
        {
            var accountNumber = Convert.ToInt32(Controls["AccountNumber"]);
            var amount = Convert.ToDecimal(Controls["Amount"]);
            Bank.Withdraw(accountNumber, amount);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Transations(int?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactions = Bank.GetAllTransactions(id.Value);
            return View(transactions);
        }


        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }
    }
}
