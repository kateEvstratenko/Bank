using System;
using System.ComponentModel.DataAnnotations;
using BankServerApi.CustomAttributes;
using BankServerApi.DataObjects.Requests.CreditRequest;
using Core.Enums;

namespace BankServerApi.DataObjects.Requests.CustomerDeposit
{
    public class AddDepositRequest
    {
        [Required]
        [Display(Name = "Сумма")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public double InitialSum { get; set; }

        [Required]
        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }

        [Required]
        [Display(Name = "Дата открытия депозита")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Период, мес")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
        public int MonthCount { get; set; }

        [Required]
        [Display(Name = "ИД депозита")]
        public int DepositId { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public CustomerBindingModel Customer { get; set; }
    }
}