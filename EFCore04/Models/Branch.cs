using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore04.Models
{
    internal class Branch
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }

        #region Relationships Navigations
        // Manager_Branch [1:1]
        public Manager BranchManager { get; set; }

        // Account_Branch [M:1]
        public ICollection<Account>? BranchAccounts { get; set; } = new HashSet<Account>();
        #endregion
    }
}
