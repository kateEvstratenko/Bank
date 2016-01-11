using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace ClientApi.Models.CalculationModels
{
    public class CalculationIncomeSumModel
    {
        [BindRequired]
        [Display(Name = "Сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double Sum { get; set; }

        [BindRequired]
        [Display(Name = "Период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public int MonthCount { get; set; }

        [BindRequired]
        [Display(Name = "ИД кредита")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int CreditId { get; set; }

        [BindRequired]
        [Display(Name = "Платежи по другим кредитам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double OtherCreditPayments { get; set; }

        [BindRequired]
        [Display(Name = "Коммунальные платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double UtilitiesPayments { get; set; }

        [BindRequired]
        [Display(Name = "Прочие платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double OtherPayments { get; set; } 
    }
}