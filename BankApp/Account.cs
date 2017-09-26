﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public enum TypeOfAccount
        {

        Checking,
        Saving,
        Loan,
        CD
}
    /// <summary>
    /// This is about a bank account
    /// </summary>
   public class Account

    {
        private static int lastAccountNumber = 0;

        #region Properties
        /// <summary>
        /// This holds the acoount number
        /// </summary>
        public int AccountNumber { get;  }
        /// <summary>
        /// This is the email address of the user
        /// </summary>
        public string EmailAddress { get; set; }
        public decimal Balance { get; private set; }
        public TypeOfAccount AccountType { get; set; }
        public DateTime CreateDate { get; set; }
        #endregion

        #region Constructors
        public Account()
        {
            //lastAccountNumber += 1;
          //  AccountNumber = lastAccountNumber;

            AccountNumber = ++lastAccountNumber;
#endregion
        }


        #region Methods
        public decimal Deposit(decimal amount)
        {
            Balance += amount; // Balance= Balance +amount
            return Balance;
        }
            

            public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }


#endregion
    }
}
