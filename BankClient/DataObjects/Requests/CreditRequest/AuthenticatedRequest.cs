using BLL.Models;
using Newtonsoft.Json;

namespace DataObjects.Requests.CreditRequest
{
    public class AuthenticatedRequest
    {
        public string Token { get; set; }

        [JsonIgnore]
        public DomainToken TokenObj { get; set; }
    }
}
