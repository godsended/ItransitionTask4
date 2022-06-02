using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ItransitionTask4.Models;
using ItransitionTask4.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ItransitionTask4.Services;

public class Auth : IAuth
{
    private readonly UserContext userDatabase;
    private readonly HttpContext httpContext;

    public Auth(UserContext userDatabase, IHttpContextAccessor httpContextAccessor)
    {
        this.userDatabase = userDatabase;
        this.httpContext = httpContextAccessor!.HttpContext!;
    }

    public string GenerateUserLoginKey()
    {
        int a = new RandomGenerator().Next(Int32.MinValue, Int32.MaxValue);
        
        SHA256 key = SHA256.Create()!;
        Encoding enc = Encoding.Default;
        byte[] byteKey = key.ComputeHash(enc.GetBytes(a.ToString()));

        StringBuilder sb = new StringBuilder();
        foreach (var b in byteKey)
        {
            sb.Append(b.ToString("x2"));
        }
        
        return sb.ToString();
    }
    public async Task<string>? Register(string name, string email, string password)
    {
        User? user = await userDatabase.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            user = new User();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.LastLoginDateTime = DateTime.Now.ToString();
            user.RegistrationDateTime = DateTime.Now.ToString();
            user.State = UserState.Common;
            user.LoginKey = GenerateUserLoginKey();
            
            userDatabase.Users.Add(user);
            await userDatabase.SaveChangesAsync();

            await Authenticate(email);

            return user.LoginKey;
        }

        return "";
    }

    public async Task<string>? Login(string email, string password)
    {
        User? user = await userDatabase.Users.FirstOrDefaultAsync(u
            => u.Email == email
               && u.Password == password);

        if (user != null && user.State!=UserState.Blocked)
        {
            await Authenticate(email);
            return user.LoginKey;
        }

        return "";
    }

    public async Task Authenticate(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, email)
        };
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<bool> TryAutoLogin(string email, string key)
    {
        User? user = await userDatabase.Users.FirstOrDefaultAsync(
            u => u.Email == email && u.LoginKey == key && u.State == UserState.Common);
        if(user!=null) 
            Debug.WriteLine(user.State.ToString()); 
        else 
            Debug.WriteLine("User is null");
        return (user != null);
    }
}