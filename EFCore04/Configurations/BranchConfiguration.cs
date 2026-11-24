using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore04.Models;

namespace EFCore04.Configurations
{
    internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Code);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(200);
            builder.Property(b => b.PhoneNumber).IsRequired();

            #region Relations
            // Manager_Branch
            builder.HasOne(b => b.BranchManager)
                .WithOne(m => m.ManagerBranch)
                .HasForeignKey<Manager>(n => n.BranchCode);

            // Account_Branch
            builder.HasMany(b => b.BranchAccounts)
                .WithOne(a => a.AccountBranch)
                .HasForeignKey(fk => fk.BranchId);
            #endregion

            #region Seed Data
            builder.HasData(new Branch
            {
                Code = "CAI-01",
                Name = "NBE",
                Address = "Cairo",
                PhoneNumber = 01016556892
            },
            new Branch
            {
                Code = "CAI-02",
                Name = "AlexBank",
                Address = "Cairo",
                PhoneNumber = 01233434099
            },
            new Branch
            {
                Code = "GZ-01",
                Name = "CIB",
                Address = "Giza",
                PhoneNumber = 0112882308
            }
            );
            #endregion
        }
    }
}
