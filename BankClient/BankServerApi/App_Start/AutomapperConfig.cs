using AutoMapper;
using BankServerApi.Models;
using BLL.Models;
using DataObjects.Requests.CreditRequest;
using DAL.Entities;

namespace BankServerApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<RegisterEmployeeModel, AppUser>();
            Mapper.CreateMap<CreditBindingModel, DomainCredit>();
            Mapper.CreateMap<AddCreditRequestModel, DomainCreditRequest>();
        }
    }
}