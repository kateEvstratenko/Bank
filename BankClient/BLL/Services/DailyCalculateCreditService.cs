﻿using System;
using System.Linq;
using System.Threading;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class DailyCalculateService
    {
        private Timer _timer;

        public DailyCalculateService()
        {
//            var dateNow = GlobalValues.BankDateTime;
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var uow = scope.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                if (uow != null)
                {
                    var dbTime = uow.GlobalValuesRepository.GetAll().FirstOrDefault();
                    if (dbTime == null)
                    {
                        var dateNow = DateTime.Now;
                        uow.GlobalValuesRepository.Add(new DAL.Entities.GlobalValues()
                        {
                            BankDateTime = dateNow
                        });
                        uow.SaveChanges();
                        GlobalValues.BankDateTime = dateNow;
                    }
                    else
                    {
                        GlobalValues.BankDateTime = dbTime.BankDateTime;   
                    }
                }
            }

            var mitnight = GlobalValues.BankDateTime.AddMinutes(1);
            var dueTime = (mitnight - GlobalValues.BankDateTime).Ticks;
            _timer = new Timer(Check, null, new TimeSpan(dueTime), TimeSpan.FromMinutes(1));
        }

        private void Check(object state)
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var uow = scope.GetService(typeof (IUnitOfWork)) as IUnitOfWork;
                if (uow != null)
                {
                    var existingValues = uow.GlobalValuesRepository.GetAll().FirstOrDefault();
                    if (existingValues != null)
                    {
                        existingValues.BankDateTime = existingValues.BankDateTime.AddDays(1);
                        GlobalValues.BankDateTime = existingValues.BankDateTime;
                    }
                    uow.SaveChanges();
                }
            }
            CheckCreditPayments();
            CheckDepositPercents();
        }

        private void CheckCreditPayments()
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var calculationDebtService = scope.GetService(typeof(ICalculationDebtService)) as ICalculationDebtService;
                if (calculationDebtService != null)
                {
                    calculationDebtService.CheckPayments();
                }
            }
        }

        private void CheckDepositPercents()
        {
            using (var scope = CustomDependencyResolver.Resolver.BeginScope())
            {
                var calculationDepositPercentService =
                    scope.GetService(typeof (ICalculationDepositPercentService)) as ICalculationDepositPercentService;
                if (calculationDepositPercentService != null)
                {
                    calculationDepositPercentService.AddPercents();
                }
            }
        }
    }
}