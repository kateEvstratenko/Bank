using System;
using BLL.Models;
using Core.Enums;

namespace DataObjects.Requests.CreditRequest
{
    public class AddCreditRequest
    {
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public double IncomeSum { get; set; }
        public double OtherCreditPayments { get; set; }
        public double UtilitiesPayments { get; set; }
        public double OtherPayments { get; set; }

        public byte[] MilitaryId { get; set; }
        public byte[] IncomeCertificate { get; set; }

        public int CreditId { get; set; }

        //Customer
        public DomainCustomer Customer { get; set; }
        //        public DateTime DateOfBirth { get; set; }
        //        public DocumentType DocumentType { get; set; }
        //        public string DocumentNumber { get; set; }
        //        public string IdentificationNumber { get; set; }
        //        public DomainAddress Address { get; set; }
        //        public DomainBankClient BankClient { get; set; }
    }
}