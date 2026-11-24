using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore04.Models
{
    internal class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        #region Relationships Navigations
        public Branch ManagerBranch { get; set; }
        public string BranchCode { get; set; }
        #endregion
    }
}
