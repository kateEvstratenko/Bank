using System;

namespace DAL.Entities
{
    public class Token: BaseEntity
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public Guid Guid { get; set; }
        public DateTime GenerationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}