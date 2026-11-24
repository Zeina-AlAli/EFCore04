using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore04.Models
{
    internal class Transaction
    {
        public int TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string? Note { get; set; }

        #region Relation Navigation
        public Account TransactionAccount { get; set; }
        public int AccountId { get; set; } // FK
        #endregion
    }
}
