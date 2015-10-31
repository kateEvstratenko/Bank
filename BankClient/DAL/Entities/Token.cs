using System;

namespace DAL.Entities
{
    public class Token: IBaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Login { get; set; }
        public Guid Guid { get; set; }
        public DateTime GenerationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}