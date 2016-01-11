using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace BankServerApi.Models.CalculationModels
{
    public class DepositModelForCapitalizationPlan
    {
        [BindRequired]
        [Display(Name = "Начальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double Sum { get; set; }

        [BindRequired]
        [Display(Name = "Процентная ставка")]
        [Range(1, 100, ErrorMessage = "Пожалуйста, введите значение в пределах 1-100.")]
        [DefaultValue(-1)]
        public double PercentRate { get; set; }

        [BindRequired]
        [Display(Name = "Период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public int MonthCount { get; set; }

        [BindRequired]
        [DataType(DataType.Date)]
        [Display(Name = "Дата начала")]
        [Range(typeof(DateTime), "1-Jan-2016", "1-Jan-2100", ErrorMessage = "Пожалуйста, введите дату с 2016 года по 1 января 2100 года.")]
        public DateTime StartDate { get; set; }

        [BindRequired]
        [Display(Name = "ИД депозита")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int DepositId { get; set; }
    }
}