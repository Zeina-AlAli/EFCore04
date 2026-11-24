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
    internal class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(b => b.Id).UseIdentityColumn();
            builder.Property(p => p.HireDate).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.FullName).IsRequired();
            builder.Property(b => b.PhoneNumber).IsRequired();

            #region Seed Data
            builder.HasData(new Manager
            {
                Id = 1,
                FullName = "Ahmed Ali",
                Email = "Ahmed@gmail.com",
                PhoneNumber = 01016998790,
                HireDate = new DateTime(2025, 1, 1),
                BranchCode = "CAI-01"
            },
            new Manager
            {
                Id = 2,
                FullName = "Samer Ali",
                Email = "Ahmed@gmail.com",
                PhoneNumber = 01016998790,
                HireDate = new DateTime(2025, 1, 1),
                BranchCode = "CAI-02"
            },
            new Manager
            {
                Id = 3,
                FullName = "Mohammed Adel",
                Email = "Mohammed@gmail.com",
                PhoneNumber = 01016886308,
                HireDate = new DateTime(2025, 2, 1),
                BranchCode = "GZ-01"
            }
            );
            #endregion
        }
    }
}
