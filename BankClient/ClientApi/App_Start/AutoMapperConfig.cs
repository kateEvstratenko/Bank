using AutoMapper;
using BLL.Models;
using ClientApi.Models.CalculationModels;

namespace ClientApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<DomainCreditPaymentPlanItem, CreditPaymentPlanViewModel>();
        }
    }
}