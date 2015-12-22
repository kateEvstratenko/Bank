using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models.CalculationModels
{
    public class CalculationCreditModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Sum { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MonthPeriod { get; set; }

        [Required]
        public int CreditId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double IncomeSum { get; set; }

        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double OtherCreditPayments { get; set; }

        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double UtilitiesPayments { get; set; }

        [DefaultValue(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double OtherPayments { get; set; } 
    }
}