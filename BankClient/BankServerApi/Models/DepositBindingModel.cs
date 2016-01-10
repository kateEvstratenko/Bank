using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
using BankServerApi.CustomAttributes;

namespace BankServerApi.Models
{
    public class DepositBindingModel
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
        public double InterestRate { get; set; }

        [BindRequired]
        [Display(Name = "Минимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double MinSum { get; set; }

        [BindRequired]
        [Display(Name = "Максимальная сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        [GreaterThan("MinSum", "Максимальная сумма должна быть меньше минимальной.")]
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
    }
}