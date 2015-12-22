using System;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const int TokenPartsCount = 4;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEncryptorService _iEncryptorService;
        private readonly IEmailSender _iEmailSender;
        public AuthenticationService(IUnitOfWork iUnitOfWork, IEncryptorService iEncryptorService, IEmailSender iEmailSender)
        {
            _iUnitOfWork = iUnitOfWork;
            _iEncryptorService = iEncryptorService;
            _iEmailSender = iEmailSender;
        }

        public string SignIn(string login, string password)
        {
            var userByLogin = _iUnitOfWork.AppUserRepository.GetAll().FirstOrDefault(x => x.UserName == login);

            ThrowIfNotRegistered(userByLogin);
            ThrowIfLockout(userByLogin);
            ThrowIfInvalidCredentials(userByLogin, password);
            ThrowIfEmailNotConfirmed(userByLogin);

            var token = GenerateTokenByEmail(login, userByLogin.Id);
            return token;
        }

        public string SignInEmployee(string login, string password)
        {
            var userByLogin = _iUnitOfWork.AppUserRepository.GetAll().FirstOrDefault(x => x.UserName == login);
            ThrowIfNotRegistered(userByLogin);
            ThrowIfInvalidEmployeeCredentials(userByLogin, password);

            var token = GenerateTokenByEmail(login, userByLogin.Id);
            return token;
        }

        public void SignOut(string token)
        {
            if (token == null)
            {
                throw BankClientException.ThrowInvalidToken();
            }
            var decryptedToken = _iEncryptorService.Decrypt(token);
            var tokenParts = ParseToken(decryptedToken);
            var tokenObject = CheckTokenParts(tokenParts);
            var databaseToken = _iUnitOfWork.TokenRepository.GetByGuid(tokenObject.Guid);
            _iUnitOfWork.TokenRepository.Delete(databaseToken.Id);
            _iUnitOfWork.SaveChanges();
        }

        public DomainToken CheckToken(string token)
        {
            if (token == null)
            {
                throw BankClientException.ThrowInvalidToken();
            }

            var decryptedToken = _iEncryptorService.Decrypt(token);
            var tokenParts = ParseToken(decryptedToken);
            var tokenObject = CheckTokenParts(tokenParts);
            CheckTokenValidity(tokenObject);

            return tokenObject;
        }

        private string GenerateTokenByEmail(string login, string userId)
        {
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;

            var tokenString = BuildTokenString(guid, login, userId, date);
            var encrypted = _iEncryptorService.Encrypt(tokenString);
            SaveToken(guid, login, userId, date);

            return encrypted;
        }

        private string BuildTokenString(Guid guid, string login, string userId, DateTime date)
        {
            return String.Format("{0}|{1}|{2}|{3}", guid, login, userId, date);
        }

        private void SaveToken(Guid guid, string login, string userId, DateTime date)
        {
            var token = new DomainToken(guid, login, userId, date);

            var existingToken = _iUnitOfWork.TokenRepository.GetByCustomerId(userId);

            if (existingToken != null)
            {
                existingToken.Guid = guid;
                existingToken.GenerationDate = date;
                existingToken.Login = login;
                existingToken.UserId = userId;
                existingToken.IsExpired = false;
            }
            else
            {
                _iUnitOfWork.TokenRepository.Add(Mapper.Map<Token>(token));
            }

            _iUnitOfWork.SaveChanges();
        }

        private string[] ParseToken(string tokenString)
        {
            var splitToken = tokenString.Split('|');
            if (splitToken.Length != TokenPartsCount)
            {
                throw TokenExpiredException.ThrowTokenExpiredException();
            }

            return splitToken;
        }

        private DomainToken CheckTokenParts(string[] tokenParts)
        {
            Guid guid;
            DateTime date;
            var isSuccessGuidParse = Guid.TryParse(tokenParts[0], out guid);
            var login = tokenParts[1];
            var userId = tokenParts[2];
            var isSuccessDateParse = DateTime.TryParse(tokenParts[3], out date);

            if (!isSuccessGuidParse || !isSuccessDateParse)
            {
                throw BankClientException.ThrowInvalidToken();
            }
            return new DomainToken(guid, login, userId, date);
        }

        private void CheckTokenValidity(DomainToken token)
        {
            var databaseToken = _iUnitOfWork.TokenRepository.GetByGuid(token.Guid);
            if (databaseToken == null || databaseToken.IsExpired)
            {
                throw TokenExpiredException.ThrowTokenExpiredException();
            }
            if (DateTime.UtcNow - databaseToken.GenerationDate <= TimeSpan.FromDays(21))
            {
                return;
            }
            ExpireToken(token);
            throw TokenExpiredException.ThrowTokenExpiredException();
        }

        private void ExpireToken(DomainToken token)
        {
            token.IsExpired = true;
            _iUnitOfWork.SaveChanges();
        }

        private void ThrowIfNotRegistered(AppUser user)
        {
            if (user == null)
            {
                throw BankClientException.ThrowUserNotRegistered();
            }
        }

        private void ThrowIfLockout(AppUser user)
        {
            if (user.LockoutEnabled)
            {
                if (DateTime.UtcNow > user.LockoutEndDateUtc)
                {
                    user.LockoutEnabled = false;
                    user.AccessFailedCount = 0;
                    _iUnitOfWork.SaveChanges();
                }
                else
                {
                    throw BankClientException.ThrowUserLockout();
                }
            }
        }

        private void ThrowIfInvalidEmployeeCredentials(AppUser user, string password)
        {
            var appUser = AppUserManagerFactory.Instance.Factory().Find(user.UserName, password);
            if (appUser == null)
            {
                throw BankClientException.ThrowInvalidCredentials();
            }
        }

        private void ThrowIfInvalidCredentials(AppUser user, string password)
        {
            var appUser = AppUserManagerFactory.Instance.Factory().Find(user.UserName, password);
            if (appUser == null)
            {
                user.AccessFailedCount += 1;
                if (user.AccessFailedCount == 3)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTime.UtcNow + TimeSpan.FromMinutes(15);
                    _iEmailSender.SendLockoutNotification(user.Email, user.UserName);
                }
                _iUnitOfWork.SaveChanges();

                throw BankClientException.ThrowInvalidCredentials();
            }
        }

        private void ThrowIfEmailNotConfirmed(AppUser user)
        {
            if (!user.EmailConfirmed)
            {
                throw BankClientException.ThrowEmailNotConfirmed();
            }
        }
    }
}
