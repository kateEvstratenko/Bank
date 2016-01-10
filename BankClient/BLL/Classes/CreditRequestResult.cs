using System.Web.Http.ModelBinding;

namespace BLL.Classes
{
    public class CreditRequestResult
    {
        public string DocPath { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}