using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAuthenticationService
    {
        string SignIn(string login, string password);
        DomainToken CheckToken(string token);
        string SignInEmployee(string login, string password);
        void SignOut(string token);
        void ChangeEmail(string userId, string newEmail, string baseUrl);
    }
}