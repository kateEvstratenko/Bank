using AutoMapper;
using BLL.Models;
using System.Linq;
using DAL.Entities;

namespace BLL.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Register()
        {
            #region DAL to BLL

            Mapper.CreateMap<Address, DomainAddress>();
            Mapper.CreateMap<AppUser, DomainAppUser>();
            Mapper.CreateMap<BankClient, DomainBankClient>();
            Mapper.CreateMap<Bill, DomainBill>();
            Mapper.CreateMap<Credit, DomainCredit>();
            Mapper.CreateMap<CreditPayment, DomainCreditPayment>();
            Mapper.CreateMap<CreditRequest, DomainCreditRequest>();
            Mapper.CreateMap<Customer, DomainCustomer>();
            Mapper.CreateMap<CustomerCredit, DomainCustomerCredit>();
            Mapper.CreateMap<CustomerDeposit, DomainCustomerDeposit>();
            Mapper.CreateMap<Deposit, DomainDeposit>();
            Mapper.CreateMap<DepositPayment, DomainDepositPayment>();
            Mapper.CreateMap<DepositRequest, DomainDepositRequest>();
            Mapper.CreateMap<PaymentType, DomainPaymentType>();
            Mapper.CreateMap<Token, DomainToken>();

            #endregion


            #region BLL to DAL

            Mapper.CreateMap<DomainAddress, Address>();
            Mapper.CreateMap<DomainAppUser, AppUser>();
            Mapper.CreateMap<DomainBankClient, BankClient>();
            Mapper.CreateMap<DomainBill, Bill>();
            Mapper.CreateMap<DomainCredit, Credit>();
            Mapper.CreateMap<DomainCreditPayment, CreditPayment>();
            Mapper.CreateMap<DomainCreditRequest, CreditRequest>();
            Mapper.CreateMap<DomainCustomer, Customer>();
            Mapper.CreateMap<DomainCustomerCredit, CustomerCredit>();
            Mapper.CreateMap<DomainCustomerDeposit, CustomerDeposit>();
            Mapper.CreateMap<DomainDeposit, Deposit>();
            Mapper.CreateMap<DomainDepositPayment, DepositPayment>();
            Mapper.CreateMap<DomainDepositRequest, DepositRequest>();
            Mapper.CreateMap<DomainPaymentType, PaymentType>();
            Mapper.CreateMap<DomainToken, Token>();

            #endregion
        }
    }
}
