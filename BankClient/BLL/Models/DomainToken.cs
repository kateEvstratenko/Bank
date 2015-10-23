using System;

namespace BLL.Models
{
    public class DomainToken : DomainBaseEntity
    {
        public DomainToken(){}
        public DomainToken(Guid guid, string login, int userId, DateTime generationDate)
        {
            Guid = guid;
            Login = login;
            UserId = userId;
            GenerationDate = generationDate;
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public Guid Guid { get; set; }
        public DateTime GenerationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}