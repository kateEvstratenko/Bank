using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
using BankServerApi.CustomAttributes;
using BankServerApi.DataObjects.Requests.CreditRequest;
using Core.Enums;

namespace BankServerApi.DataObjects.Requests.CustomerDeposit
{
    public class AddDepositRequest
    {
        [BindRequired]
        [Display(Name = "Сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double InitialSum { get; set; }

        [BindRequired]
        [Display(Name = "Валюта")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, выберите валюту.")]
        public Currency Currency { get; set; }

//        [Required]
//        [Display(Name = "Дата открытия депозита")]
//        [DataType(DataType.Date)]
//        public DateTime StartDate { get; set; }

        [BindRequired]
        [Display(Name = "Период, мес")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public int MonthCount { get; set; }

        [BindRequired]
        [Display(Name = "ИД депозита")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int DepositId { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public CustomerBindingModel Customer { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        public string Email { get; set; }
    }
}