using System;
using BLL.Models;
using BLL.Services;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new CreditDocService().FillConcreteCreditContract(new DomainCustomerCredit()
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
                    Name = "Прозрачный"
                }
            });
        }
    }
}
