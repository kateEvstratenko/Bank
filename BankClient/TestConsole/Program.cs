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

            /*new CreditRequestDocService().FillConcreteContract(new DomainCreditRequest()
            {
                Credit = new DomainCredit()
                {
                    Name = "Прозрачный"                   
                },
                Customer = new DomainCustomer()
                {
                    Lastname = "Иванов",
                    Firstname = "Иван",
                    Patronymic = "Иванович",
                    DateOfBirth = new DateTime(1979, 3, 15),
                    Address = new DomainAddress()
                    {
                        City = "Минск",
                        Street = "Калинина",
                        House = "56",
                        Flat = 34
                    },
                    DocumentNumber = "MP1234567"
                },
                Sum = 5000000
            });
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
            });*/

            var s = new CalculationDepositService();
            var p = s.CalculateCapitalizationPlan(10000000, 24, 13, new DateTime(2016, 1, 31));
            foreach(var pp in p)
            {
                Console.WriteLine(pp.MainSum + " " + pp.PercentSum + " " + pp.StartDate);
            }
        }
    }
}
