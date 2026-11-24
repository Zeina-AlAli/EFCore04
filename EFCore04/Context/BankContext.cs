using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EFCore04.Models;

namespace EFCore04.Context
{
    internal class BankContext : DbContext
    {
        #region Connection String
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        #endregion

        #region Model Configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region DbSet Properties
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountCustomerJunction> AccountCustomers { get; set; }
        #endregion




    }
}
