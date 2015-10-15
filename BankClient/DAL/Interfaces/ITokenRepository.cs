using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ITokenRepository: IRepository<Token>
    {
        Token GetByCustomerId(int customerIds);
        Token GetByGuid(Guid guid);
    }
}