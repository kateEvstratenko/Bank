﻿using System.Data.Entity;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class BankContext : IdentityDbContext<AppUser>
    {
        public BankContext() : base("BankContext") { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BankClient> BankClients { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditRequest> CreditRequests { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositRequest> DepositRequests { get; set; }
        public DbSet<CreditPayment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerCredit> CustomerCredits { get; set; }
        public DbSet<CustomerDeposit> CustomerDeposits { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<GlobalValues> GlobalValues { get; set; }
    }
}
