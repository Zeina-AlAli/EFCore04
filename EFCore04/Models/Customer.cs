using System;
using System.Collections.Generic;
using System.Text;
using EFCore04.Enums;

namespace EFCore04.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public CustomerType CutomerType { get; set; }
        public string NationalId { get; set; }

        #region Relationship Navigation
        // Account_Customer [M:M]
        public ICollection<AccountCustomerJunction> CustomerAccounts { get; set; } = new HashSet<AccountCustomerJunction>(); 
        #endregion
    }
}
