using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Models;
using ClientApi.Models;
using ClientApi.Models.CalculationModels;
using DAL.Entities;
using WebGrease.Css.Extensions;

namespace ClientApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<DomainCreditPaymentPlanItem, CreditPaymentPlanViewModel>();
            Mapper.CreateMap<DomainCustomerCredit, ShortCustomerCredit>();
            Mapper.CreateMap<DomainCustomerDeposit, ShortCustomerDeposit>();
            Mapper.CreateMap<DomainCredit, ShortCredit>();
            Mapper.CreateMap<DomainDeposit, ShortDeposit>();
            Mapper.CreateMap<DomainCustomer, ShortCustomer>();
            Mapper.CreateMap<DomainAddress, ShortAddress>();
            Mapper.CreateMap<CustomerCredit, ShortCustomerCredit>().AfterMap((source, dest) =>
            {
                var allSum = source.CreditPaymentPlanItems.Where(p => !p.IsPaid)
                    .Sum(x => x.MainSum + x.PercentSum);
                var allPaymentsByMainSum = source.CreditPaymentPlanItems
                    .Select(x => x.CreditPayments.Where(y => y != null)
                        .Sum(p => p.MainSum + p.PercentSum)).Sum();
                var allDebt = source.CreditPaymentPlanItems.Select(x => x.Debt).Sum(x =>
                {
                    if (x != null)
                    {
                        return x.MainSum + x.PercentSum;
                    }
                    return 0;
                });
                var allPaymentsByDebts = source.CreditPaymentPlanItems
                    .Select(x => x.CreditPayments.Where(y => y != null)
                        .Sum(p => p.DelayMainSum + p.DelayPercentSum)).Sum();
                dest.RemainSum = allSum + allDebt - allPaymentsByMainSum - allPaymentsByDebts;
            });

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
            
            Mapper.CreateMap<CustomPagedList<CustomerCredit>, CustomPagedList<ShortCustomerCredit>>();

            Mapper.CreateMap<Credit, ShortCredit>().ReverseMap();
            Mapper.CreateMap<Deposit, ShortDeposit>().ReverseMap();
            Mapper.CreateMap<Customer, ShortCustomer>().ReverseMap();
            Mapper.CreateMap<Address, ShortAddress>().ReverseMap();

            Mapper.CreateMap<RegisterBindingModel, Customer>().ReverseMap();
            Mapper.CreateMap<RegistreUserAddressViewModel, Address>().ReverseMap();
        }
    }
}