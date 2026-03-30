using webbir.Models;

namespace webbir.Interfaces;

public interface IAuthService
{
    User? Login(string username, string password);
    bool Register(string username, string email, string password);
    User? GetUserByUsername(string username);
}