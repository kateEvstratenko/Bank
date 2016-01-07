using System;

namespace Core
{
    public class BankClientException : Exception
    {
        public int Code { get; set; }
        protected BankClientException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public static BankClientException ThrowWrongTokenPartsCount()
        {
            return new BankClientException(2, "Tokenparts count is wrong.");
        }

        public static BankClientException ThrowInvalidToken()
        {
            return new BankClientException(3, "Token is invalid.");
        }

        public static BankClientException ThrowEmployeeIsNull()
        {
            return new BankClientException(4, "Employee is null.");
        }

        public static BankClientException ThrowInvalidCredentials()
        {
            return new BankClientException(5, "Credentials are invalid.");
        }

        public static BankClientException ThrowEmailNotConfirmed()
        {
            return new BankClientException(6, "Email is not confirmed.");
        }

        public static BankClientException ThrowAutofacError(string message)
        {
            return new BankClientException(7, message);
        }
        public static BankClientException ThrowAuthorizationError()
        {
            return new BankClientException(8, "Authorization has been denied for this request.");
        }

        public static BankClientException ThrowUserNotRegistered()
        {
            return new BankClientException(9, "User is not registered.");
        }

        public static BankClientException ThrowUserLockout()
        {
            return new BankClientException(10, "User is lockout.");
        }

        public static BankClientException ThrowCannotSetStatus()
        {
            return new BankClientException(11, "Can't set status for this credit request.");
        }

        public static BankClientException ThrowNotPayment()
        {
            return new BankClientException(12, "Срок приема платежа еще не наступил или платежей не существует.");
        }

        public static BankClientException ThrowUserCreditNotFound()
        {
            return new BankClientException(13, "Кредита с таким номером договора не существует.");
        }
    }
}