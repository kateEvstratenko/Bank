using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CreditRequestService : ICreditRequestService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IImageService _iImageService;
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

        public List<DomainCreditRequest> GetUnconfirmed(string roleId)
        {
            return Mapper.Map<IEnumerable<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(cr => cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(roleId))
                    .Where(cred => cred.CreditRequestStatuses
                        .Select(crs => crs.Info)
                        .Contains(CreditRequestStatusInfo.None)))
                .ToList();
        }
    }
}