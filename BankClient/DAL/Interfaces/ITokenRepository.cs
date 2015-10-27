using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ITokenRepository: IRepository<Token>
    {
        Token GetByCustomerId(string customerIds);
        Token GetByGuid(Guid guid);
    }
}