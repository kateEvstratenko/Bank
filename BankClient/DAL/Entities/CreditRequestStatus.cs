using Core.Enums;
using Microsoft.Owin.Security;

namespace DAL.Entities
{
    public class CreditRequestStatus: IBaseEntity
    {
        public int Id { get; set; }
        public CreditRequestStatusInfo Info { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public int CreditRequestId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual CreditRequest CreditRequest { get; set; }
    }
}