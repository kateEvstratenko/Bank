using BLL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankServerApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SumRangeAttribute : ValidationAttribute
    {
        private string otherPropertyName;

        public ICreditService creditService;

        public SumRangeAttribute(string otherPropertyName) : base()
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var a = CustomDependencyResolver.Resolver;
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                var sum = (double)value;
                var creditId = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                var credit = creditService.Get(creditId);
                if (sum < credit.MinSum || sum > credit.MaxSum)
                {
                    var errorMessage = String.Format("Sum should be between {0} and {1}.", credit.MinSum, credit.MaxSum);
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