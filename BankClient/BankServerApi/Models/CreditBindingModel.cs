﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Web.ModelBinding;
using BankServerApi.CustomAttributes;
using BLL.Models;

namespace BankServerApi.Models
{
    public class CreditBindingModel
    {
        [Required]
        [Display(Name = "Название")]
        [StringLength(100, ErrorMessage = "Поле {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [StringLength(1000, ErrorMessage = "Поле {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
        public string Description { get; set; }

        [BindRequired]
        [Display(Name = "Процентная ставка")]
        [Range(1, 100, ErrorMessage = "Пожалуйста, введите значение в пределах 1-100.")]
        [DefaultValue(-1)]
        public double PercentRate { get; set; }

        [BindRequired]
        [Display(Name = "Минимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double MinSum { get; set; }

        [BindRequired]
        [Display(Name = "Максимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        [GreaterThan("MinSum", "Максимальная сумма должна быть больше минимальной.")]
        public double MaxSum { get; set; }

        [BindRequired]
        [Display(Name = "Минимальный период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public int MinMonthPeriod { get; set; }

        [BindRequired]
        [Display(Name = "Максимальный период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        [GreaterThan("MinMonthPeriod", "Максимальный период должен быть больше минимального.")]
        public int MaxMonthPeriod { get; set; }

        [BindRequired]
        [Display(Name = "ИД типа платежа")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int PaymentTypeId { get; set; }
    }
}