using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore04.Enums;

namespace EFCore04.Models
{
    internal class AccountCustomerJunction
    {
        public int AccNum { get; set; }
        public int CustId { get; set; }
        public DateTime OwnershipStartDate { get; set; }
        public OwnershipRole Role { get; set; }
        public Status AccountStatus { get; set; }

        #region Relationship Navigations
        // With Account
        public Account account { get; set; }

        // With Customer
        public Customer customer { get; set; }
        #endregion

    }
}
