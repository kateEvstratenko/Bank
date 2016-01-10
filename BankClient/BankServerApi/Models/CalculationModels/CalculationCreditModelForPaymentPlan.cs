﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace BankServerApi.Models.CalculationModels
{
    public class CalculationCreditModelForPaymentPlan
    {
        [BindRequired]
        [Display(Name = "Сумма")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double Sum { get; set; }

        [BindRequired]
        [Display(Name = "Период, мес")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0")]
        [DefaultValue(0)]
        public int MonthCount { get; set; }

        [BindRequired]
        [Display(Name = "ИД кредита")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int CreditId { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Дата начала")]
        [Range(typeof(DateTime), "1-Jan-2016", "1-Jan-2100")]
        public DateTime StartDate { get; set; }
    }
}