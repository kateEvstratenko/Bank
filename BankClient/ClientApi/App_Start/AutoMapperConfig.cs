using AutoMapper;
using BLL.Classes;
using BLL.Models;
using ClientApi.Models.CalculationModels;
using DAL.Entities;

namespace ClientApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<DomainCreditPaymentPlanItem, CreditPaymentPlanViewModel>();
            Mapper.CreateMap<DomainCustomerCredit, ShortCustomerCredit>();
            Mapper.CreateMap<DomainCredit, ShortCredit>();
            Mapper.CreateMap<DomainDeposit, ShortDeposit>();

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

            Mapper.CreateMap<Credit, ShortCredit>().ReverseMap();
            Mapper.CreateMap<Deposit, ShortDeposit>().ReverseMap();
        }
    }
}