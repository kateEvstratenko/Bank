using BLL.Models;

namespace BLL.Interfaces
{
    public interface IRegistrationService
    {
        void RegisterEmployee(AppUserBll bankEmployee);
    }

    public interface IAuthenticationService
    {
        string SignIn(string login, string password);
        TokenBll CheckToken(string token);
    }
}