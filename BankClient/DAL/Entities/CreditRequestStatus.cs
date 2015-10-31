using Core.Enums;

namespace DAL.Entities
{
    public class CreditRequestStatus: IBaseEntity
    {
        public int Id { get; set; }
        public CreditRequestStatusInfo Info { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}