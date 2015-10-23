using AutoMapper;
using BankServerApi.Models;
using BLL.Models;
using DAL.Entities;

namespace BankServerApi
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
//            Mapper.CreateMap<RegisterEmployeeModel, AppUserBll>();
            Mapper.CreateMap<RegisterEmployeeModel, AppUser>();
        }
    }
}