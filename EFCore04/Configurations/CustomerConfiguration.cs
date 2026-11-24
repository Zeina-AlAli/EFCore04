using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore04.Enums;

namespace EFCore04.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(b => b.Id).UseIdentityColumn();
            builder.Property(b => b.CutomerType).HasDefaultValue(CustomerType.Individual);
            builder.Property(b => b.FullName).IsRequired();
            builder.Property(b => b.PhoneNumber).IsRequired();
            builder.Property(b => b.NationalId).IsRequired();

        }
    }
}
