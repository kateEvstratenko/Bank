using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.ModelBinding;
using BankServerApi.CustomAttributes;
using BLL.Models;
using Core.Enums;

namespace BankServerApi.DataObjects.Requests.CreditRequest
{
    public class AddCreditRequest
    {
        [Required]
        [Display(Name = "Цель кредита")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string CreditGoal { get; set; }

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
        [Display(Name = "Валюта")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, выберите валюту.")]
//        [DefaultValue()]
        public Currency Currency { get; set; }

        [BindRequired]
        [Display(Name = "Заработная плата")]
        [Range(1, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение больше 0.")]
        [DefaultValue(0)]
        public double IncomeSum { get; set; }

        [BindRequired]
        [Display(Name = "Ежемесячные выплаты по другим кредитам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double OtherCreditPayments { get; set; }

        [BindRequired]
        [Display(Name = "Ежемесячные выплаты по коммунальным платежам")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double UtilitiesPayments { get; set; }

        [BindRequired]
        [Display(Name = "Прочие платежи")]
        [Range(0, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение не менее 0.")]
        [DefaultValue(-1)]
        public double OtherPayments { get; set; }

//        [Required]
        [Display(Name = "Копия военного билета")]
        public string MilitaryId { get; set; }

        [Required]
        [Display(Name = "Копия справки о доходах")]
        public string IncomeCertificate { get; set; }

        [BindRequired]
        [Display(Name = "ИД кредита")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public int CreditId { get; set; }

        //Customer
//        [Required]
        [Display(Name = "Клиент")]
        public CustomerBindingModel Customer { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
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
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Lastname { get; set; }

//        [Required]
        [Display(Name = "Отчество")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Patronymic { get; set; }

        [BindRequired]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DateRangeAttribute]
        public DateTime DateOfBirth { get; set; }

        [BindRequired]
        [Display(Name = "Тип документа, удостоверяющего личность")]
        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(Name = "Номер документа")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        [RegularExpression(@"^\w{2}\d{7}$", ErrorMessage = "Неверный формат.")]
        public string DocumentNumber { get; set; }

        [Required]
        [Display(Name = "Идентификационный номер")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        [RegularExpression(@"^\d{7}\w\d{3}\w{2}\d$", ErrorMessage = "Неверный формат.")]
        public string IdentificationNumber { get; set; }

//        [Required]
        [Display(Name = "Адрес")]
        public AddressBindingModel Address { get; set; }
    }

    public class AddressBindingModel
    {
        [Required]
        [Display(Name = "Страна")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Город")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Улица")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Дом")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string House { get; set; }

        [Display(Name = "Квартира")]
        public int? Flat { get; set; }
    }
}