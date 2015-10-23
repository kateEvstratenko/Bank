using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAuthenticationService
    {
        string SignIn(string login, string password);
        DomainToken CheckToken(string token);
    }
}