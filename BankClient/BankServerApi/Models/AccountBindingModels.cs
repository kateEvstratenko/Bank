using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.ModelBinding;
using Core.Enums;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankServerApi.Models
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

    public class RegisterEmployeeModel
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

        [Required]
        [Display(Name = "Имя")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Lastname { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(100, ErrorMessage = "Превышена максимально допустимая длина 100 символов.")]
        public string Patronymic { get; set; }

        [BindRequired]
        [Display(Name = "Роль")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите значение.")]
        [DefaultValue(-1)]
        public AppRoles Role { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ShortAppUser : IdentityUser, IBaseEntity
    {
        int IBaseEntity.Id { get; set; }
        public int? CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string RoleName { get; set; }
    }
}
