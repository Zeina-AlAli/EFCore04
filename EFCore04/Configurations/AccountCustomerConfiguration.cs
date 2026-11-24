using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore04.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore04.Configurations
{
    internal class AccountCustomerConfiguration : IEntityTypeConfiguration<AccountCustomerJunction>
    {
        public void Configure(EntityTypeBuilder<AccountCustomerJunction> builder)
        {
            builder.HasKey(c => new { c.AccNum, c.CustId });

            #region Relationships
            // with Account
            builder.HasOne(n => n.account)
                .WithMany(m => m.AccountCustomers)
                .HasForeignKey(k => k.AccNum);

            // With Customer
            builder.HasOne(n => n.customer)
              .WithMany(m => m.CustomerAccounts)
              .HasForeignKey(k => k.CustId);
            #endregion
        }


    }
}
