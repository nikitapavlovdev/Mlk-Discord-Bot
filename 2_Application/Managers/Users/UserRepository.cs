using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._3_Infrastructure.DataBase;

namespace MlkAdmin._2_Application.Managers.Users
{
    public class UserRepository(
        MlkAdminDbContext mlkAdminDbContext,
        ILogger<UserRepository> logger) : IUserRepository
    {
        public async Task UpsertUserAsync(User user)
        {
            try
            {
                User? dbUser = await mlkAdminDbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

                if(dbUser == null)
                {
                    await mlkAdminDbContext.Users.AddAsync(user);
                }
                else
                {
                    mlkAdminDbContext.Entry(dbUser).CurrentValues.SetValues(user);
                }

                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
        public async Task DeleteDbUserAsync(ulong id)
        {
            try
            {
                User? dbUser = await mlkAdminDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (dbUser == null) { return; }

                mlkAdminDbContext.Users.Remove(dbUser);
                await mlkAdminDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
