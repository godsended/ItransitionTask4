using Microsoft.EntityFrameworkCore;

namespace ItransitionTask4.Models;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=task4;Username=postgres;Password=174285396020");
    }

    public async Task DeleteUsersByIds(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            User? user = await Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                Users.Remove(user);
            }
        }

        await SaveChangesAsync();
    }
    
    public async Task BlockUsersByIds(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            User? user = await Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.State = UserState.Blocked;
            }
        }

        await SaveChangesAsync();
    }
    
    public async Task UnblockUsersByIds(IEnumerable<int> ids)
    {
        foreach (var id in ids)
        {
            User? user = await Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.State = UserState.Common;
            }
        }

        await SaveChangesAsync();
    }
}