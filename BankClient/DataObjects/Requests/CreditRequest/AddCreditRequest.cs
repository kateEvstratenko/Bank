using BLL.Models;
using Core.Enums;

namespace DataObjects.Requests.CreditRequest
{
    public class AddCreditRequest: AuthenticatedRequest
    {
        public AddCreditRequestModel CreditRequest { get; set; }
    }

    public class AddCreditRequestModel
    {
        public string CreditGoal { get; set; }
        public double Sum { get; set; }
        public Currency Currency { get; set; }
        public int CreditId { get; set; }
    }
}