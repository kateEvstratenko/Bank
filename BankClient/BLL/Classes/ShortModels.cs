using System;
using Core.Enums;

namespace BLL.Classes
{
    public class ShortCustomerCredit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double CreditSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsClosed { get; set; }
        public int CustomerId { get; set; }
        public int CreditId { get; set; }
        public int BillId { get; set; }
        public double RemainSum { get; set; }
    }

    public class ShortCustomerDeposit
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public double InitialSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get;set; }

        public int CustomerId { get; set; }
        public ShortCustomer Customer { get; set; }
        public int DepositId { get; set; }
        public ShortDeposit Deposit { get; set; }
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
        public ShortCustomer Customer { get; set; }
        public int CreditId { get; set; }
        public ShortCredit Credit { get; set; }
    }

    public class ShortCreditPayment
    {
        public int Id { get; set; }
        public double MainSum { get; set; }
        public double PercentSum { get; set; }
        public double DelayMainSum { get; set; }
        public double DelayPercentSum { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateTime { get; set; }
        public int CreditPaymentPlanItemId { get; set; }
        public int SourceBillId { get; set; }
        public int DestinationBillId { get; set; }
    }

    public class ShortCustomer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public ShortAddress Address { get; set; }
    }

    public class ShortAddress
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Flat { get; set; }
    }
}