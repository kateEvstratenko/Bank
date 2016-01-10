using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BankServerApi.CustomAttributes;
using BLL.Models;
using Core.Enums;

namespace BankServerApi.DataObjects.Requests.CreditRequest
{
    public class AddCreditRequest
    {
        [Required]
        [Display(Name = "Цель кредита")]
        public string CreditGoal { get; set; }

        [Required]
        [Display(Name = "Сумма")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]
//        [LessThan("MaxSum", "Сумма должна быть меньше максимальной.")]
        public double Sum { get; set; }

        [Required]
        [Display(Name = "Период, мес")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]      
        public int MonthCount { get; set; }

        [Required]
        [Display(Name = "Валюта")]
        public Currency Currency { get; set; }

        [Required]
        [Display(Name = "Заработная плата")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]        
        public double IncomeSum { get; set; }

        [Required]
        [Display(Name = "Ежемесячные выплаты по другим кредитам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]   
        public double OtherCreditPayments { get; set; }

        [Required]
        [Display(Name = "Ежемесячные выплаты по коммунальным платежам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]   
        public double UtilitiesPayments { get; set; }

        [Required]
        [Display(Name = "Прочие платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше, чем {1}.")]   
        public double OtherPayments { get; set; }

//        [Required]
        [Display(Name = "Копия военного билета")]
        public string MilitaryId { get; set; }

        [Required]
        [Display(Name = "Копия справки о доходах")]
        public string IncomeCertificate { get; set; }

        [Required(ErrorMessage = "Требуется поле")]
        [Display(Name = "ИД кредита")]
        [DefaultValue(0)]
        public int CreditId { get; set; }

        //Customer
        [Required]
        [Display(Name = "Клиент")]
        public CustomerBindingModel Customer { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        public string Email { get; set; }
        //        public DateTime DateOfBirth { get; set; }
        //        public DocumentType DocumentType { get; set; }
        //        public string DocumentNumber { get; set; }
        //        public string IdentificationNumber { get; set; }
        //        public DomainAddress Address { get; set; }
        //        public DomainBankClient BankClient { get; set; }
    }

    public class CustomerBindingModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

//        [Required]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Тип документа, удостоверяющего личность")]
        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(Name = "Номер документа")]
        public string DocumentNumber { get; set; }

        [Required]
        [Display(Name = "Идентификационный номер")]
        public string IdentificationNumber { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public AddressBindingModel Address { get; set; }
    }

    public class AddressBindingModel
    {
        [Required]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Дом")]
        public string House { get; set; }

        [Display(Name = "Квартира")]
        public int? Flat { get; set; }
    }
}