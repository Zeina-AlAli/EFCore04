using EFCore04.Context;
using EFCore04.Enums;
using EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;
using System.Xml.Serialization;

namespace EFCore04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using BankContext context = new BankContext();
            int choice;
            do
            {

                Menu();
                do
                {
                    Console.Write("Enter Choise: ");
                } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 5);

                switch (choice)
                {
                    case 1:
                        AddCustomer(context);
                        Pause();

                        break;

                    case 2:
                        OpenNewAccount(context);
                        Pause();
                        break;

                    case 3:
                        UpdateAccountStatus(context);
                        Pause();
                        break;

                    case 4:
                        RemoveAccountFromCustomer(context);
                        Pause();
                        break;

                    case 5:
                        LisrCustomers(context);
                        Pause();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Choice!");
                        Pause();
                        break;

                }
            } while (choice != 0);
        }

        #region Menu
        static void Menu()
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("\t\tNational Bank Management");
            Console.WriteLine("=========================================================");
            Console.WriteLine("1) Add a new Customer.");
            Console.WriteLine("2) Open a new Account for  Customer.");
            Console.WriteLine("3) Update Account Status ( Active / Closed)");
            Console.WriteLine("4) Remove an Account for a Customer.");
            Console.WriteLine("5) List all Customer (With Accounts).");
            Console.WriteLine("0) Exist");
            Console.WriteLine("---------------------------------------------------------");
        }
        #endregion

        #region Pause Return to menu
        static void Pause()
        {
            Console.WriteLine("Press any key to return tp the menu... ");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion

        #region Add Customer
        static void AddCustomer(BankContext context)
        {
            Console.Clear();
            Console.WriteLine("\n--- Add New Customer ---");
            Console.Write("Full Name       : ");
            string Name = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(Name))
            {
                Console.Write("Name cannot be empty. Enter Full Name: ");
                Name = Console.ReadLine()!;
            }

            Console.Write("National ID     : ");
            string NID = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(NID) || NID.Length != 14 || !NID.All(char.IsDigit))
            {
                Console.Write("Invalid National ID. Enter a valid 14-digit ID: ");
                NID = Console.ReadLine()!;
            }
            Console.Write("Date of Birth   (yyyy-MM-dd): ");
            DateOnly DOB;
            while (!DateOnly.TryParse(Console.ReadLine(), out DOB) || DOB >= DateOnly.FromDateTime(DateTime.Today))
            {
                Console.Write("Invalid date. Enter a valid date (yyyy-MM-dd): ");
            }

            Console.Write("Email           : ");
            string email = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(email))
            {
                Console.Write(" Input Cant be Empty. Please Enter Your Email: ");
                email = Console.ReadLine()!;
            }

            Console.Write("Phone           : ");
            int phone ;
            while(!int.TryParse(Console.ReadLine(), out phone))
            {
                Console.Write("Input Cant be Empty. Enter Phone Number Agian: ");
            }

            Console.Write("Address         : ");
            string address = Console.ReadLine()!;
            while (string.IsNullOrWhiteSpace(address))
            {
                Console.Write("Address cannot be empty. Enter Address: ");
                address = Console.ReadLine()!;
            }

            Console.WriteLine("Customer Type:");
            Console.WriteLine("    1) Individual");
            Console.WriteLine("    2) Business");
            Console.Write("Choice: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.Write("Invalid Choice. Enter 1 or 2: ");
            }

            Customer c = new Customer()
            {
                FullName = Name,
                NationalId = NID,
                Address = address,
                Email = email,
                PhoneNumber = phone,
                DateOfBirth = DOB,
                CutomerType = choice == 1 ? CustomerType.Individual : CustomerType.Business
            };
            context.Customers.Add(c);
            context.SaveChanges();
            Console.WriteLine($"\nCustomer created successfully. CustomerId = {c.Id}");
        }
        #endregion

        #region Open Account
        static void OpenNewAccount(BankContext context)
        {
            Console.Clear();
            Console.WriteLine("\n--- Open New Account ---");

            Console.Write("Account Number  : ");
            int num; 
            while(!int.TryParse(Console.ReadLine(), out num) || context.Accounts.Any(n => n.AccountNumber == num))
            {
                Console.Write("Invalid input or account number already exists. Please enter another number: ");
            }

            Console.WriteLine("Account Type:");
            Console.WriteLine("    1) Savings");
            Console.WriteLine("    2) Current");
            Console.WriteLine("    3) Business");
            Console.Write("Choice: ");

            int AccountType;
            while(!int.TryParse(Console.ReadLine(), out AccountType) || AccountType < 1 || AccountType > 3)
            {
                Console.Write("Invalid Choice, Please Enter Your choice (1, 2 or 3): ");
            }


            Console.Write("Branch Code     : ");
            string code = Console.ReadLine()!;
            while ( String.IsNullOrWhiteSpace(code) || !context.Branches.Any(n => n.Code == code))
            {
                Console.Write(" Input cant be Empty or Branch not found. Enter another Branch Code: ");
                code = Console.ReadLine()!;
            }

            Console.Write("Customer Id     : ");
            int customerId ;
            while ( !int.TryParse(Console.ReadLine(), out customerId) || !context.Customers.Any(c => c.Id == customerId))
            {
                Console.Write("Invalid Input. Enter a valid Customer Id: ");
            }

            Console.WriteLine("Ownership Role:");
            Console.WriteLine("    1) Primary");
            Console.WriteLine("    2) CoHolder");
            Console.Write("Choice: ");

            int roleChoice;
            while (!int.TryParse(Console.ReadLine(), out roleChoice) ||
                   roleChoice < 1 || roleChoice > 2)
            {
                Console.Write("Invalid choice. Enter 1 or 2: ");

            }

            Console.WriteLine("\nValidating branch and customer...");
            Account acc = new Account()
            {
                AccountNumber = num,
                accountType = AccountType==1? Enums.AccountType.Savings: AccountType == 2 ? Enums.AccountType.Current : Enums.AccountType.Business,
                BranchId = code,
                CurrentBalance = 0,
                OpeningDay = DateTime.Now
            };
            AccountCustomerJunction AccCust = new AccountCustomerJunction()
            {
                CustId = customerId,
                AccNum = num,
                AccountStatus = Status.Active,
                Role = roleChoice == 1 ? OwnershipRole.Primary : OwnershipRole.CoHolder,
                OwnershipStartDate = DateTime.Now
            };

            context.Accounts.Add(acc);
            context.AccountCustomers.Add(AccCust);
            context.SaveChanges();
            Console.WriteLine("Account created and linked successfully.");

        }
        #endregion

        #region Update Account Status
        static void UpdateAccountStatus(BankContext context)
        {
            Console.Clear();
            Console.WriteLine("\n--- Update Account Status ---");
            Console.Write("Account Number  : ");
            int accNum ;
            while(!int.TryParse(Console.ReadLine(), out accNum) || !context.AccountCustomers.Any(n => n.AccNum == accNum))
            {
                Console.Write("Account not Exist, Please Enter Valid Account: ");
            }
            Console.Write("Customer Id     : ");
            int custId ;
            while(!int.TryParse(Console.ReadLine(), out custId) || !context.AccountCustomers.Any(n => n.CustId == custId))
            {
                Console.Write("Invalid Customer Id. Enter again: ");
            }
            Console.WriteLine("New Status:");
            Console.WriteLine("    1) Active");
            Console.WriteLine("    2) Closed");
            Console.Write("Choice: ");
            int choice ;
            while(!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.Write("Invalid choice, Enter again 1/2");
                
            }
            AccountCustomerJunction Acc = new()
            {
                AccNum = accNum,
                CustId =custId,

            };
            var account = context.AccountCustomers.Where(c => c.AccNum == accNum && c.CustId == custId)
                .FirstOrDefault();
            if (account is null)
            {
                Console.WriteLine("Account not found");
                return;
            }
            account.AccountStatus = choice == 1 ? Status.Active : Status.Active;
            context.SaveChanges();
            Console.WriteLine("\nStatus updated successfully.");
        }
        #endregion

        #region Remove an Account from a Customer
        static void RemoveAccountFromCustomer(BankContext context)
        {
            Console.Clear();
            Console.WriteLine("\n--- Remove Account From Customer ---");
            Console.Write("Account Number  : ");
            int accNum = int.Parse(Console.ReadLine()!);
            while (!context.AccountCustomers.Any(n => n.AccNum == accNum))
            {
                Console.Write("Account not Exist, Please Enter Valid Account: ");
                accNum = int.Parse(Console.ReadLine()!);
            }
            Console.Write("Customer Id     : ");
            int custId ;
            while (!int.TryParse(Console.ReadLine(), out custId) || !context.AccountCustomers.Any(n => n.CustId == custId))
            {
                Console.Write("Invalid Customer Id. Enter again: ");
            }
            var account = context.AccountCustomers.Where(c => c.AccNum == accNum && c.CustId == custId)
                .FirstOrDefault();
            if (account is null)
            {
                Console.WriteLine($"This account doesnt belong to Customer {custId}");
                return;
            }
            context.AccountCustomers.Remove(account);
            context.SaveChanges();
            Console.WriteLine("\nOwnership link deleted.");
            var AccountOwners = context.AccountCustomers.Count(c => c.AccNum == accNum);
            if(AccountOwners == 0)
            {
            Console.WriteLine("Account was also removed because it had no remaining owners.");
            }
        }
        #endregion
        
        #region List All Customers
        static void LisrCustomers(BankContext context)
        {
            Console.Clear();
            var customers = context.Customers.Include(c => c.CustomerAccounts)
                .ThenInclude(a => a.account)
                .ThenInclude(b => b.AccountBranch).ToList();

            if(customers is null)
            {
                Console.WriteLine("No Customers Found");
                return;
            }
            foreach( var customer in customers)
            {
                Console.WriteLine($"#{customer.Id} {customer.FullName} ({customer.CutomerType})");
                if(!customer.CustomerAccounts.Any())
                {
                    Console.WriteLine("Customer doesnt have any Account");
                    continue;
                }
                foreach (var ca in customer.CustomerAccounts)
                {
                    Console.WriteLine($"{ca.AccNum} Current Balance : {ca.account.CurrentBalance} {ca.account.accountType} {ca.AccountStatus} @{ca.account.AccountBranch.Name}");
                }
                    
            }
        }
        #endregion
    }
}
