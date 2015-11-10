using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    public class CreditRequestService : ICreditRequestService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IImageService _iImageService;

        private static readonly List<AppRoles> RolesToConfirm = new List<AppRoles>()
        {
            AppRoles.CreditCommitteeMember,
            AppRoles.CreditDepartmentChief,
            AppRoles.Security
        };

        public CreditRequestService(IUnitOfWork iUnitOfWork, IImageService iImageService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iImageService = iImageService;
        }

        public void Add(DomainCreditRequest creditRequest, string userId, byte[] militaryId, byte[] incomeCertificate, string baseUrl)
        {
            var customer = _iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var creditRequestDal = Mapper.Map<CreditRequest>(creditRequest);
            creditRequest.CustomerId = customer.Id;

            var militaryPath = _iImageService.SaveImageFromByteArray(militaryId, baseUrl, userId, ImageType.MilitaryId);
            creditRequest.MilitaryIdPath = militaryPath;
            var incomeSertificatePath = _iImageService.SaveImageFromByteArray(incomeCertificate, baseUrl, userId, ImageType.IncomeCertificate);
            creditRequest.IncomeCertificatePath = incomeSertificatePath;

            _iUnitOfWork.CreditRequestRepository.Add(creditRequestDal);
            _iUnitOfWork.SaveChanges();
        }

        public List<DomainCreditRequest> GetUnconfirmed(IdentityRole role)
        {
            if (!RolesToConfirm.Select(r => r.ToString()).Contains(role.Name))
            {
                throw BankClientException.ThrowAuthorizationError();
            }
            return Mapper.Map<IEnumerable<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(cr => !cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(role.Id))
                //.Where(cred => cred.CreditRequestStatuses
                //  .Select(crs => crs.Info)
                //.Contains(CreditRequestStatusInfo.None)))
                        )
                .ToList();
        }
    }
}