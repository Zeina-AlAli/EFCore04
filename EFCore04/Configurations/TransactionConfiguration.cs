using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFCore04.Models;

namespace EFCore04.Configurations
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(k => k.TransactionNumber);
            builder.Property(i => i.TransactionNumber).UseIdentityColumn();
            builder.Property(d => d.TransactionDate).HasDefaultValueSql("GETDATE()");

        }
    }
    
}
