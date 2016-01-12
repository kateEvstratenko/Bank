using System.Web.Http.ModelBinding;

namespace BLL.Interfaces
{
    public interface IValidationService
    {
        void ValidateSum(double sum, double minSum, double maxSum, ModelStateDictionary modelState, bool isInitial = false);

        void ValidateMonthCount(int monthCount, int minMonthPeriod, int maxMonthPeriod, ModelStateDictionary modelState);
    }
}