using BLL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using BLL.Services;

namespace BankServerApi.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SumRangeAttribute : ValidationAttribute
    {
        private string otherPropertyName;
        
        public SumRangeAttribute(string otherPropertyName) : base()
        {
            this.otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);
                var sum = (double)value;
                var creditId = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                using (var scope = CustomDependencyResolver.Resolver.BeginScope())
                {
                    var creditService = scope.GetService(typeof (ICreditService)) as ICreditService;
                    if (creditService == null)
                    {
                        validationResult = new ValidationResult("Cannot resolve ICreditService");
                    }
                    else
                    {
                        var credit = creditService.Get(creditId);
                        if (sum < credit.MinSum || sum > credit.MaxSum)
                        {
                            var errorMessage = String.Format("Sum should be between {0} and {1}.", credit.MinSum,
                                credit.MaxSum);
                            validationResult = new ValidationResult(errorMessage);
                        }
                    }            
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