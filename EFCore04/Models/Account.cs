using EFCore04.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore04.Models
{
    internal class Account
    {
        public int AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountType accountType { get; set; }
        public DateTime OpeningDay { get; set; }

        #region Relationships Navigations
        // Branch_Account [1:M]
        public Branch AccountBranch { get; set; }
        public string BranchId { get; set; } // FK

        // Transaction_Account [M:1]
        public ICollection<Transaction>? AccountTransactions { get; set; } = new HashSet<Transaction>();

        // Customer_Account [M:M]
        public ICollection<AccountCustomerJunction> AccountCustomers { get; set; } = new HashSet<AccountCustomerJunction>();
        #endregion
    }
}
