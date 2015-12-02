﻿using System;
using System.Data.SqlClient;
using AutoMapper;
using BankServerApi.Models;
using BankServerApi.Models.CalculationModels;
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
            Mapper.CreateMap<DepositBindingModel, DomainDeposit>();
            Mapper.CreateMap<AddCreditRequest, DomainCreditRequest>();

            Mapper.CreateMap<DomainCreditPaymentPlanItem, CreditPaymentPlanViewModel>();
        }
    }
}   
