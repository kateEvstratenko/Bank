using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAuthenticationService
    {
        string SignIn(string login, string password);
        TokenBll CheckToken(string token);
    }
}