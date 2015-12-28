using System;
using System.Collections.Generic;
using BLL.Models;
using BLL.Services;
using PagedList;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
//            var a = new [] { 1, 2, 3 };
//            var b = a.ToPagedList<int>(1, 10);
            new CreditDocService().FillConcreteContract(new DomainCustomerCredit()
            {
                ContractNumber = "67836458457899",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(365 * 5),
                CreditSum = 50000000,
                Customer = new DomainCustomer()
                {
                    Firstname = "Екатерина",
                    Lastname = "Евстратенко"
                },
                Credit = new DomainCredit()
                {
                    Name = "Прозрачный",
                    PercentRate = 45
                }
            });

            new DepositDocService().FillConcreteContract(new DomainCustomerDeposit()
            {
                ContractNumber = "67836458457899",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now + TimeSpan.FromDays(365 * 5),
                InitialSum = 50000000,
                Customer = new DomainCustomer()
                {
                    Firstname = "Екатерина",
                    Lastname = "Евстратенко"
                },

                Deposit = new DomainDeposit()
                {
                    Name = "Прозрачный",
                    InterestRate = 30
                }
            });
        }
    }
}
