using System.Collections.Generic;
using BLL.Models;
using Core;

namespace BLL
{
    public class ParsedTokenHelper
    {
        public DomainToken GetParsedToken(IDictionary<string, object> requestProperties)
        {
            object tokenObj;
            requestProperties.TryGetValue("tokenObj", out tokenObj);
            if (tokenObj == null)
            {
                throw BankClientException.ThrowAuthorizationError();
            }
            var token = tokenObj as DomainToken;
            if (token == null)
            {
                throw BankClientException.ThrowAuthorizationError();
            }
            return token;
        }
    }
}