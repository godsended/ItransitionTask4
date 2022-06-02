using ItransitionTask4.Models;

namespace ItransitionTask4.Services;

public interface IAuth
{
    /// <summary>
    /// If successful return UserLoginKey
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<string> Register(string name, string email, string password);
    /// <summary>
    /// If successful return UserLoginKey
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<string> Login(string email, string password);

    Task<bool> TryAutoLogin(string email, string key);
    public string GenerateUserLoginKey();
    public Task Authenticate(string email);
}