using System.Linq;
using AutoMapper;
using BLL.Classes;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using Core;
using Core.Enums;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        public void Add(DomainCreditRequest creditRequest, byte[] militaryId, byte[] incomeCertificate, string baseUrl)
        {
            var customer = creditRequest.Customer;//_iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var customerDb = _iUnitOfWork.CustomerRepository.GetAll()
                .FirstOrDefault(c => c.IdentificationNumber == creditRequest.Customer.IdentificationNumber);

            if (customerDb == null)
            {
                customerDb = Mapper.Map<Customer>(customer);
                _iUnitOfWork.CustomerRepository.Add(customerDb);
                _iUnitOfWork.SaveChanges();
            }

            creditRequest.CustomerId = customerDb.Id;

            var militaryPath = _iImageService.SaveImageFromByteArray(militaryId, baseUrl, customerDb.Id, ImageType.MilitaryId);
            creditRequest.MilitaryIdPath = militaryPath;
            var incomeSertificatePath = _iImageService.SaveImageFromByteArray(incomeCertificate, baseUrl, customerDb.Id, ImageType.IncomeCertificate);
            creditRequest.IncomeCertificatePath = incomeSertificatePath;
            
            var creditRequestDal = Mapper.Map<CreditRequest>(creditRequest);
            _iUnitOfWork.CreditRequestRepository.Add(creditRequestDal);
            _iUnitOfWork.SaveChanges();

            new CreditRequestDocService().FillConcreteContract(creditRequest);
        }

        public CustomPagedList<DomainCreditRequest> GetUnconfirmed(IdentityRole role, int pageNumber, int pageSize)
        {
            return Mapper.Map<CustomPagedList<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(cr => !cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(role.Id)).ToCustomPagedList(pageNumber, pageSize));
        }

        public CustomPagedList<DomainCreditRequest> GetСonfirmed(string appUserId, IdentityRole chiefRole, int pageNumber, int pageSize)
        {
            return Mapper.Map<CustomPagedList<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(cr => cr.CreditRequestStatuses
                    .Select(s => s.AppUserId)
                    .Contains(appUserId)
                    && !cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(chiefRole.Id)).ToCustomPagedList(pageNumber, pageSize));
        }

        public CustomPagedList<DomainCreditRequest> GetUnconfirmedByChief(IdentityRole role, int pageNumber, int pageSize)
        {
            return Mapper.Map<CustomPagedList<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(cr => !cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(role.Id)).ToCustomPagedList(pageNumber, pageSize));
        }

        public CustomPagedList<DomainCreditRequest> GetConfirmedByChief(string appUserId, int pageNumber, int pageSize)
        {
            return Mapper.Map<CustomPagedList<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                    .Where(cr => cr.CreditRequestStatuses
                        .Select(s => s.AppUserId)
                        .Contains(appUserId) && cr.Credit == null).ToCustomPagedList(pageNumber, pageSize));
        }

        public void SetStatus(string userId, int creditRequestId, CreditRequestStatusInfo statusInfo, string message)
        {
            var creditRequest = _iUnitOfWork.CreditRequestRepository.Get(creditRequestId);

            //не выдали ли уже кредит
            if (creditRequest.Credit != null)
            {
                throw BankClientException.ThrowCannotSetStatus();
            }

            var chiefRoleName = AppRoles.CreditDepartmentChief.ToString();
            //не обработал ли заяку начальник
            if (!AuthManagerService.UserManager.IsInRole(userId, chiefRoleName))
            {
                if (creditRequest.CreditRequestStatuses.Any(s => AuthManagerService.UserManager.IsInRole(s.AppUserId, chiefRoleName)))
                {
                    throw BankClientException.ThrowCannotSetStatus();
                }
            }

            var existingStatus = creditRequest.CreditRequestStatuses.FirstOrDefault(cs => cs.AppUserId == userId);
            if (existingStatus != null)
            {
                existingStatus.Info = statusInfo;
                existingStatus.Message = message;
            }
            else
            {
                creditRequest.CreditRequestStatuses.Add(new CreditRequestStatus()
                {
                    AppUserId = userId,
                    Info = statusInfo,
                    Message = message
                });
            }
            _iUnitOfWork.SaveChanges();
        }
    }
}