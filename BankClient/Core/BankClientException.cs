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
    }
}