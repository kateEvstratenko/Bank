using System;
using System.ComponentModel.DataAnnotations;
using Core.Enums;

namespace ClientApi.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeEmailBindingModel
    {
        [Required]
        [Display(Name = "Новый адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Невалидный адрес электронной почты.")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string NewEmail { get; set; }

        [Required]
        [Display(Name = "Подтверждение адреса электронной почты")]
//        [EmailAddress(ErrorMessage = "Невалидный адрес электронной почты.")]
        [Compare("NewEmail", ErrorMessage = "Новый адрес и подтверждение адреса не совпадают.")]
        public string ConfirmEmail { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Email { get; set; }

//        [Required]
//        [Display(Name = "Имя")]
//        public string Firstname { get; set; }
//
//        [Required]
//        [Display(Name = "Фамилия")]
//        public string Lastname { get; set; }
//
//        [Display(Name = "Отчество")]
//        public string Patronymic { get; set; }
//
//        [Required]
//        [Display(Name = "Дата рождения")]
//        public DateTime DateOfBirth { get; set; }
//
//        [Required]
//        [Display(Name = "Тип документа, удостоверяющего личность")]
//        public DocumentType DocumentType { get; set; }

        [Required]
        [Display(Name = "Идентификационный номер")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        [RegularExpression(@"^\d{7}\w\d{3}\w{2}\d$", ErrorMessage = "Поле должно иметь формат: 0000000A000AA0.")]
        public string IdentificationNumber { get; set; }

//        [Required]
//        [Display(Name = "Адрес")]
//        public RegistreUserAddressViewModel Address { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Суперсекретный код")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Code { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
