using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<AppUserBll, AppUser>().ReverseMap();
            Mapper.CreateMap<CreditRequestBll, CreditRequest>().ReverseMap();
        }
    }
}
