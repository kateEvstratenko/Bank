using System.Web.Http.ModelBinding;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ValidationService : IValidationService
    {

        public void ValidateSum(double sum, double minSum, double maxSum, ModelStateDictionary modelState)
        {
            if (sum < minSum)
            {
                modelState.Add("Sum", new ModelState() { Errors = { "����� �� ����� ���� ������ �����������." } });
                //                throw BankClientException.ThrowSumLessThanMin();
            }

            if (sum > maxSum)
            {
                modelState.Add("Sum", new ModelState() { Errors = { "����� �� ����� ���� ������ ������������." } });
                //                throw BankClientException.ThrowSumMoreThanMax();
            }
        }

        public void ValidateMonthCount(int monthCount, int minMonthPeriod, int maxMonthPeriod, ModelStateDictionary modelState)
        {
            if (monthCount < minMonthPeriod)
            {
                modelState.Add("MonthCount", new ModelState() { Errors = { "���������� ������� �� ����� ���� ������ ������������." } });
                //                throw BankClientException.ThrowMonthLessThanMin();
            }

            if (monthCount > maxMonthPeriod)
            {
                modelState.Add("MonthCount", new ModelState() { Errors = { "���������� ������� �� ����� ���� ������ �������������." } });
            }
        }
    }
}