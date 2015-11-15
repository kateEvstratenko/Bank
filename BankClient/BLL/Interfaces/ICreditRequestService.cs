using System.Collections.Generic;
using BLL.Models;
using Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(DomainCreditRequest creditRequest, byte[] militaryId, byte[] incomeCertificate, string baseUrl);
        List<DomainCreditRequest> GetUnconfirmed(IdentityRole role);
        void SetStatus(string userId, int creditRequestId, CreditRequestStatusInfo statusInfo, string message);
    }
}