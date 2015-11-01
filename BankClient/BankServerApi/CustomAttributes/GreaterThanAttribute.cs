using System;
using System.ComponentModel.DataAnnotations;

namespace BankServerApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class GreaterThanAttribute : ValidationAttribute
    {
        string otherPropertyName;

        public GreaterThanAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                var propertyType = otherPropertyInfo.PropertyType;
                dynamic toValidate = Convert.ChangeType(value, propertyType);
                dynamic referenceProperty = Convert.ChangeType(otherPropertyInfo.GetValue(validationContext.ObjectInstance, null), propertyType);
                if (toValidate.CompareTo(referenceProperty) < 1)
                {
                    validationResult = new ValidationResult(ErrorMessageString);
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