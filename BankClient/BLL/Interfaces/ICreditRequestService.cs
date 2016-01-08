using BLL.Classes;
using BLL.Models;
using Core.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Interfaces
{
    public interface ICreditRequestService
    {
        void Add(DomainCreditRequest creditRequest, byte[] militaryId, byte[] incomeCertificate, string email, string baseUrl, string baseLocalhostUrl);
        CustomPagedList<DomainCreditRequest> GetUnconfirmed(IdentityRole role, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> GetConfirmed(string appUserId, IdentityRole chiefRole, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> GetUnconfirmedByChief(IdentityRole role, int pageNumber, int pageSize);
        CustomPagedList<DomainCreditRequest> GetConfirmedByChief(string appUserId, int pageNumber, int pageSize);
        void SetStatus(string userId, int creditRequestId, CreditRequestStatusInfo statusInfo, string message);
        string GetContract(int id, string baseUrl);
    }
}