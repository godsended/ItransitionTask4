using ItransitionTask4.Models;
using Microsoft.EntityFrameworkCore;

namespace ItransitionTask4.Services;

public class OnlineStatusUpdater : IOnlineStatusUpdater
{
    private UserContext userDatabase;
    private IActions actions;

    public OnlineStatusUpdater(UserContext userDatabase, IActions actions)
    {
        this.userDatabase = userDatabase;
        this.actions = actions;
    }

    public async void UpdateOnlineTime(string email)
    {
        User? user = await userDatabase.Users.FirstOrDefaultAsync(u => email == u.Email);
        if (user != null)
        {
            user.LastLoginDateTime = DateTime.Now.ToString();
            userDatabase.Users.Update(user);
            await userDatabase.SaveChangesAsync();
        }
    }
    
}