using System;
using Core;

namespace DataObjects.Responses
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            IsSuccess = true;
        }

        protected ResponseBase(bool success)
        {
            IsSuccess = success;
        }

        public bool IsSuccess { get; set; }

        public Error ErrorMessage { get; set; }
        public Message Message { get; set; }

        public bool IsTokenExpired { get; set; }

        public static ResponseBase Unsuccessful(BankClientException ex)
        {
            return new ResponseBase()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = ex.Code,
                    Description = ex.Message
                }
            };
        }

        public static ResponseBase Warning(Message message)
        {
            return new ResponseBase()
            {
                IsSuccess = true,
                Message = new Message()
                {
                    Code = message.Code,
                    Description = message.Description
                }
            };
        }

        public static TResponse Unsuccessful<TResponse>(BankClientException ex)
            where TResponse : ResponseBase, new()
        {
            return new TResponse()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = ex.Code,
                    Description = ex.Message
                }
            };
        }

        public static TResponse Unsuccessful<TResponse>(Exception exception)
            where TResponse : ResponseBase, new()
        {
            return new TResponse()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = -1,
                    Description = String.Format("Unknown error: {0}", exception.Message)
                }
            };
        }

        public static ResponseBase Unsuccessful(Exception exception)
        {
            return new ResponseBase()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = -1,
                    Description = String.Format("Unknown error: {0}", exception.Message)
                }
            };
        }

        public static TResponse TokenExpired<TResponse>()
            where TResponse : ResponseBase, new()
        {
            return new TResponse()
            {
                IsSuccess = false,
                IsTokenExpired = true
            };
        }

        public static ResponseBase TokenExpired()
        {
            return new ResponseBase()
            {
                IsSuccess = false,
                IsTokenExpired = true
            };
        }
    }

    public class ResponseBase<T> : ResponseBase
    {
        protected ResponseBase(bool success = true)
            : base(success)
        {
        }

        public ResponseBase(T data, bool success = true)
            : base(success)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static new ResponseBase<T> Unsuccessful(BankClientException ex)
        {
            return new ResponseBase<T>()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = ex.Code,
                    Description = ex.Message
                }
            };
        }
        public static new ResponseBase<T> Unsuccessful(Exception exception)
        {
            return new ResponseBase<T>()
            {
                IsSuccess = false,
                ErrorMessage = new Error()
                {
                    Code = -1,
                    Description = String.Format("Unknown error: {0}", exception.Message)
                }
            };
        }
        public static new ResponseBase<T> TokenExpired()
        {
            return new ResponseBase<T>()
            {
                IsSuccess = false,
                IsTokenExpired = true
            };
        }
    }
}
