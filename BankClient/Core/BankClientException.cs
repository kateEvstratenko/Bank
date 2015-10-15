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
    }
}