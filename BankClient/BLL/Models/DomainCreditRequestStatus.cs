using Core.Enums;

namespace BLL.Models
{
    public class DomainCreditRequestStatus
    {
        public int Id { get; set; }
        public CreditRequestStatusInfo Info { get; set; }
        public string Message { get; set; }
    }
}