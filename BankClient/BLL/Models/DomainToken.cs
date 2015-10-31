using System;

namespace BLL.Models
{
    public class DomainToken : IDomainBaseEntity
    {
        public DomainToken(){}
        public DomainToken(Guid guid, string login, string userId, DateTime generationDate)
        {
            Guid = guid;
            Login = login;
            UserId = userId;
            GenerationDate = generationDate;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Login { get; set; }
        public Guid Guid { get; set; }
        public DateTime GenerationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}