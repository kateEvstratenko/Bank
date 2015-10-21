using System;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Core;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RegistrationService : IRegistrationService 
    {
         private readonly IUnitOfWork _iUnitOfWork;
         public RegistrationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public void RegisterEmployee(AppUserBll bankEmployee)
        {
            var employee = _iUnitOfWork.AppUserRepository.GetByEmail(bankEmployee.Id);
            employee.Firstname = bankEmployee.Firstname;
            employee.Lastname = bankEmployee.Lastname;
            _iUnitOfWork.SaveChanges();
        }
    }

    public class AuthenticationService : IAuthenticationService
    {
        private const int TokenPartsCount = 4;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IEncryptorService _iEncryptorService;
        public AuthenticationService(IUnitOfWork iUnitOfWork, IEncryptorService iEncryptorService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iEncryptorService = iEncryptorService;
        }

        public string SignIn(string login, string password)
        {
            //todo verify login-password
            var userId = 0;
            var token = GenerateTokenByEmail(login, userId);
            return token;
        }

        public TokenBll CheckToken(string token)
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

        private string GenerateTokenByEmail(string login, int userId)
        {
            var guid = Guid.NewGuid();
            var date = DateTime.UtcNow;

            var tokenString = BuildTokenString(guid, login, userId, date);
            var encrypted = _iEncryptorService.Encrypt(tokenString);
            SaveToken(guid, login, userId, date);

            return encrypted;
        }

        private string BuildTokenString(Guid guid, string login, int userId, DateTime date)
        {
            return String.Format("{0}|{1}|{2}|{3}", guid, login, userId, date);
        }

        private void SaveToken(Guid guid, string login, int userId, DateTime date)
        {
            var token = new TokenBll(guid, login, userId, date);

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

        private TokenBll CheckTokenParts(string[] tokenParts)
        {
            Guid guid;    
            int userId;
            DateTime date;
            var isSuccessGuidParse = Guid.TryParse(tokenParts[0], out guid);
            var login = tokenParts[1];
            var isSuccessUserIdParse = Int32.TryParse(tokenParts[2], out userId);
            var isSuccessDateParse = DateTime.TryParse(tokenParts[3], out date);

            if (!isSuccessGuidParse || !isSuccessDateParse || !isSuccessUserIdParse)
            {
                throw BankClientException.ThrowInvalidToken();
            }
            return new TokenBll(guid, login, userId, date);
        }

        private void CheckTokenValidity(TokenBll token)
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

        private void ExpireToken(TokenBll token)
        {
            token.IsExpired = true;
            _iUnitOfWork.SaveChanges();
        }
    }
}
