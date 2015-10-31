using System;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class TokenRepository: BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(BankContext context) : base(context)
        {
        }

        public Token GetByCustomerId(string customerIds)
        {
            return Context.Tokens.FirstOrDefault(t => t.UserId == customerIds);
        }

        public Token GetByGuid(Guid guid)
        {
            return Context.Tokens.FirstOrDefault(t => t.Guid == guid);
        }
    }
}