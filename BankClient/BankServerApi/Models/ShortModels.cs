using System;
using Core.Enums;

namespace BankServerApi.Models
{
    public class ShortCustomerCredit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int BillId { get; set; } 
    }

    public class ShortCustomerDeposit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CustomerId { get; set; }
        public int DepositId { get; set; }
        public int BillId { get; set; }
    }

    public class ShortCredit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double PercentRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
        public int PaymentTypeId { get; set; }
    }

    public class ShortDeposit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double InterestRate { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int MinMonthPeriod { get; set; }
        public int MaxMonthPeriod { get; set; }
    }

    public class ShortCreditRequest
    {
        public int Id { get; set; }
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public int MonthCount { get; set; }
        public Currency Currency { get; set; }
        public double IncomeSum { get; set; }
        public double OtherCreditPayments { get; set; }
        public double UtilitiesPayments { get; set; }
        public double OtherPayments { get; set; }
        public string MilitaryIdPath { get; set; }
        public string IncomeCertificatePath { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
    }
}