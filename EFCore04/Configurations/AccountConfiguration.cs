using EFCore04.Enums;
using EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore04.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(i => i.AccountNumber);
            builder.Property(a => a.AccountNumber).ValueGeneratedNever();
            builder.Property(p => p.OpeningDay).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.accountType).HasDefaultValue(AccountType.Savings);            
            builder.ToTable(t => t.HasCheckConstraint("CHK_Balance", "CurrentBalance >= 0"));

            #region Relationships
            // Transaction_Account [M:1]
            builder.HasMany(a => a.AccountTransactions)
                .WithOne(t => t.TransactionAccount)
                .HasForeignKey(f => f.AccountId);


            #endregion


        }
    }
}
