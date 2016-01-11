using System;
using System.ComponentModel.DataAnnotations;
using BLL.Interfaces;
using BLL.Services;

namespace BankServerApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateRangeAttribute : ValidationAttribute
    {
//        private string otherPropertyName;

        public DateRangeAttribute()
            : base()
        {
            //            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var date = (DateTime)value;
                var currentDate = GlobalValues.BankDateTime;
                var minDate = new DateTime(1900, 1, 1);
                if (date > currentDate || date < minDate)
                {
                    var errorMessage = String.Format("Дата должна быть в пределах {0:dd.MM.yyyy} и {1:dd.MM.yyyy}.",
                        minDate.Date, currentDate.Date);
                    validationResult = new ValidationResult(errorMessage);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validationResult;
        }
    }
}