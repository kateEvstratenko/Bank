using BLL.Models;
using DAL.Entities;

namespace DataObjects.Requests.CreditRequest
{
    public class AuthenticatedRequest
    {
        public string Token { get; set; }

        [JsonIgnore]
        public TokenBll TokenObj { get; set; }
    }
}
