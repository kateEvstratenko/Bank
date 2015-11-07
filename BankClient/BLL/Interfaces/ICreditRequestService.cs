using System.Collections.Generic;
using BLL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(DomainCreditRequest creditRequest, string userId, byte[] militaryId, byte[] incomeCertificate, string baseUrl);
        List<DomainCreditRequest> GetUnconfirmed(IdentityRole role);
    }
}