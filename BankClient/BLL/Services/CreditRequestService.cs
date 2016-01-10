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
        private readonly ICustomerService _iCustomerService;

        public CreditRequestService(IUnitOfWork iUnitOfWork, IImageService iImageService, ICustomerService iCustomerService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iImageService = iImageService;
            _iCustomerService = iCustomerService;
        }

        public string Add(DomainCreditRequest creditRequest, byte[] militaryId, byte[] incomeCertificate, string email, string baseUrl, string baseLocalhostUrl)
        {
            creditRequest.Credit = Mapper.Map<DomainCredit>(_iUnitOfWork.CreditRepository.Get(creditRequest.CreditId));
            Validate(creditRequest);

            var customer = creditRequest.Customer;//_iUnitOfWork.CustomerRepository.GetCustomerByUserId(userId);
            var customerDb = _iUnitOfWork.CustomerRepository.GetAll()
                .FirstOrDefault(c => c.IdentificationNumber == creditRequest.Customer.IdentificationNumber);

            if (customerDb == null)
            {
                var id = _iCustomerService.Register(customer, email);
                creditRequest.CustomerId = id;
            }
            else
            {
                creditRequest.CustomerId = customerDb.Id;
            }

            if (militaryId != null)
            {
                var militaryPath = _iImageService.SaveImageFromByteArray(militaryId, baseUrl, creditRequest.CustomerId,
                    ImageType.MilitaryId, baseLocalhostUrl);
                creditRequest.MilitaryIdPath = militaryPath;
            }
            var incomeSertificatePath = _iImageService.SaveImageFromByteArray(incomeCertificate, baseUrl, creditRequest.CustomerId, ImageType.IncomeCertificate, baseLocalhostUrl);
            creditRequest.IncomeCertificatePath = incomeSertificatePath;

            var creditRequestDal = Mapper.Map<CreditRequest>(creditRequest);
            creditRequestDal.Credit = null;
            creditRequestDal.Customer = null;
            _iUnitOfWork.CreditRequestRepository.Add(creditRequestDal);
            _iUnitOfWork.SaveChanges();

            new CreditRequestDocService().FillConcreteContract(creditRequest);
            return GetContract(creditRequestDal.Id, baseLocalhostUrl);
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

        public CustomPagedList<DomainCreditRequest> GetConfirmed(string appUserId, IdentityRole chiefRole, int pageNumber, int pageSize)
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
                _iUnitOfWork.CreditRequestRepository.GetAll().ToList()
                .Where(cr => cr.CreditRequestStatuses.Any(s => AuthManagerService.UserManager.IsInRole(s.AppUserId, AppRoles.Security.ToString()))
                && cr.CreditRequestStatuses.Any(s => AuthManagerService.UserManager.IsInRole(s.AppUserId, AppRoles.CreditCommitteeMember.ToString())))
                .Where(cr => !cr.CreditRequestStatuses
                    .SelectMany(s => s.AppUser.Roles)
                    .Select(r => r.RoleId)
                    .Contains(role.Id))
                    .AsQueryable()
                    .ToCustomPagedList(pageNumber, pageSize));
        }

        public CustomPagedList<DomainCreditRequest> GetConfirmedByChief(string appUserId, int pageNumber, int pageSize)
        {
            var chiefRoleName = AppRoles.CreditDepartmentChief.ToString();
            return Mapper.Map<CustomPagedList<DomainCreditRequest>>(
                _iUnitOfWork.CreditRequestRepository.GetAll()
                .Where(c => !c.CustomerCredits.Any()).ToList()
                .Where(c => c.CreditRequestStatuses.Any(s => AuthManagerService.UserManager.IsInRole(s.AppUserId, chiefRoleName)))
                .AsQueryable().ToCustomPagedList(pageNumber, pageSize));
        }

        public void SetStatus(string userId, int creditRequestId, CreditRequestStatusInfo statusInfo, string message)
        {
            var creditRequest = _iUnitOfWork.CreditRequestRepository.Get(creditRequestId);

            //не выдали ли уже кредит
            if (creditRequest.CustomerCredits.Any())
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
                if (statusInfo == CreditRequestStatusInfo.None)
                {
                    _iUnitOfWork.CreditRequestStatusRepository.Delete(existingStatus.Id);
                }
                else
                {
                    existingStatus.Info = statusInfo;
                    existingStatus.Message = message;
                }
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

        public string GetContract(int id, string baseLocalhostUrl)
        {
            return string.Format("{0}/Content/CreditRequestContracts/{1}.docx", baseLocalhostUrl, id);
        }

        private void Validate(DomainCreditRequest creditRequest)
        {
            if (creditRequest.Sum < creditRequest.Credit.MinSum)
            {
                throw BankClientException.ThrowSumLessThanMin();
            }

            if (creditRequest.Sum > creditRequest.Credit.MaxSum)
            {
                throw BankClientException.ThrowSumMoreThanMax();
            }

            if (creditRequest.MonthCount < creditRequest.Credit.MinMonthPeriod)
            {
                throw BankClientException.ThrowMonthLessThanMin();
            }

            if (creditRequest.MonthCount > creditRequest.Credit.MaxMonthPeriod)
            {
                throw BankClientException.ThrowMonthMoreThanMax();
            }
        }
    }
}