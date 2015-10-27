using System;

namespace BLL.Models
{
    public class TokenBll : BaseEntityBll
    {
        public TokenBll(){}
        public TokenBll(Guid guid, string login, string userId, DateTime generationDate)
        {
            Guid = guid;
            Login = login;
            UserId = userId;
            GenerationDate = generationDate;
        }

        public string UserId { get; set; }
        public string Login { get; set; }
        public Guid Guid { get; set; }
        public DateTime GenerationDate { get; set; }
        public bool IsExpired { get; set; }
    }
}