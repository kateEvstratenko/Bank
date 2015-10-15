namespace Core
{
    public class TokenExpiredException : BankClientException
    {
        private TokenExpiredException(int code, string message)
            : base(code, message)
        {
            Code = code;
        }

        public static TokenExpiredException ThrowTokenExpiredException()
        {
            return new TokenExpiredException(1, "Token expired.");
        }
    }
}