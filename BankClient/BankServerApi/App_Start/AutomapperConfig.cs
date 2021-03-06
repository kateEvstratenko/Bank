﻿using System.Linq;
using AutoMapper;
using BankServerApi.DataObjects.Requests.CreditRequest;
using BankServerApi.DataObjects.Requests.CustomerDeposit;
using BankServerApi.Models;
using BankServerApi.Models.CalculationModels;
using BLL.Classes;
using BLL.Models;
using DAL.Entities;

namespace BankServerApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<RegisterEmployeeModel, AppUser>();
            Mapper.CreateMap<CreditBindingModel, DomainCredit>();
            Mapper.CreateMap<DepositBindingModel, DomainDeposit>();
            Mapper.CreateMap<AddCreditRequest, DomainCreditRequest>();
            Mapper.CreateMap<AddDepositRequest, DomainCustomerDeposit>();
            Mapper.CreateMap<CreditRequestStatus, DomainCreditRequestStatus>().ReverseMap();

            Mapper.CreateMap<DomainCreditPaymentPlanItem, CreditPaymentPlanViewModel>();
            Mapper.CreateMap<DomainCustomerCredit, ShortCustomerCredit>();
            Mapper.CreateMap<DomainCustomerDeposit, ShortCustomerDeposit>();
            Mapper.CreateMap<DomainCredit, ShortCredit>();
            Mapper.CreateMap<DomainCreditRequest, ShortCreditRequest>();
            Mapper.CreateMap<DomainDeposit, ShortDeposit>();
            Mapper.CreateMap<DomainCustomer, ShortCustomer>();
            Mapper.CreateMap<DomainAddress, ShortAddress>();

            Mapper.CreateMap<CustomPagedList<CustomerCredit>, CustomPagedList<DomainCustomerCredit>>();
            Mapper.CreateMap<CustomPagedList<DomainCustomerCredit>, CustomPagedList<ShortCustomerCredit>>();

            Mapper.CreateMap<CustomPagedList<CustomerDeposit>, CustomPagedList<DomainCustomerDeposit>>();
            Mapper.CreateMap<CustomPagedList<DomainCustomerDeposit>, CustomPagedList<ShortCustomerDeposit>>();

            Mapper.CreateMap<CustomPagedList<Credit>, CustomPagedList<DomainCredit>>();
            Mapper.CreateMap<CustomPagedList<DomainCredit>, CustomPagedList<ShortCredit>>();

            Mapper.CreateMap<CustomPagedList<Deposit>, CustomPagedList<DomainDeposit>>();
            Mapper.CreateMap<CustomPagedList<DomainDeposit>, CustomPagedList<ShortDeposit>>();

            Mapper.CreateMap<CustomPagedList<CreditRequest>, CustomPagedList<DomainCreditRequest>>();
            Mapper.CreateMap<CustomPagedList<DomainCreditRequest>, CustomPagedList<ShortCreditRequest>>();
           
            Mapper.CreateMap<CustomPagedList<AppUser>, CustomPagedList<ShortAppUser>>();
            Mapper.CreateMap<CustomPagedList<CustomerCredit>, CustomPagedList<ShortCustomerCredit>>();


            Mapper.CreateMap<DomainCustomer, CustomerBindingModel>().ReverseMap();
            Mapper.CreateMap<DomainAddress, AddressBindingModel>().ReverseMap();
            Mapper.CreateMap<Credit, ShortCredit>().ReverseMap();
            Mapper.CreateMap<Deposit, ShortDeposit>().ReverseMap();
            Mapper.CreateMap<Customer, ShortCustomer>().ReverseMap();
            Mapper.CreateMap<Address, ShortAddress>().ReverseMap();
            Mapper.CreateMap<AppUser, ShortAppUser>().ReverseMap();
        }
    }
}   
