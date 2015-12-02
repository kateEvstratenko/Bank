using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Models;
using BLL.Services;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            new DocService().FillConcreteContract(new DomainCustomerCredit()
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
