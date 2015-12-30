using BLL.Classes;
using BLL.Models;
using Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(DomainCreditRequest creditRequest, byte[] militaryId, byte[] incomeCertificate, string baseUrl);
        CustomPagedList<DomainCreditRequest> GetUnconfirmed(IdentityRole role, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> Get—onfirmed(string appUserId, IdentityRole chiefRole, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> GetUnconfirmedByChief(IdentityRole role, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> GetConfirmedByChief(string appUserId, int pageNumber, int pageSize);
        void SetStatus(string userId, int creditRequestId, CreditRequestStatusInfo statusInfo, string message);
    }
}